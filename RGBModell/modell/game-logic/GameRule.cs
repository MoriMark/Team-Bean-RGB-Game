using RGBModel.modell.exceptions;
using RGBModell.modell.boxlogic;
using RGBModell.modell.enums;
using RGBModell.modell.events;
using RGBModell.modell.exceptions;
using RGBModell.modell.game_logic;
using RGBModell.modell.gameobjects;
using RGBModell.modell.structs;
using Task = RGBModell.modell.structs.Task;

namespace RGBModell.modell.game_logic
{
    public class GameRule
    {
        private Field field;

        private List<Robot> robots;
        private Robot currentRobot;

        private Dictionary<int, BoxGroup> boxgroups;

        Int32 numberOfCurrentRound;
        public Boolean GameIsActive { get; private set; }
        public Boolean GameIsPaused { get; private set; }
        public List<Exit> exits { get; private set; }

        private TaskHandler taskHandler;
        private const Int32 TASK_MAX_TASKS_COUNT = 5;
        private const Int32 TASK_MIN_TASKS_COUNT = 2;
        private const Int32 TASK_CHANCE_OF_NEW_TASK = 10;

        public event EventHandler<UpdateFieldsEventArgs> UpdateFields;

        public event EventHandler<UpdateTasksEventArgs> UpdateTasks
        {
            add { taskHandler.UpdateFields += value; }
            remove { taskHandler.UpdateFields -= value; }
        }

        public event EventHandler<SpecialEventEventArgs> SpecialEvents;
        public GameRule(Int32 numOfRobots, Int32 numOfTeams)
        {
            GameIsActive = false;
            GameIsPaused = false;
            int totalRobots = numOfRobots * numOfTeams;
            int tableSize = totalRobots*4;
            field = new Field(tableSize);
            robots = field.GenerateField(numOfRobots, numOfTeams);
            boxgroups = new Dictionary<int, BoxGroup>();
            exits = field.exits;
            taskHandler = new TaskHandler();
        }

        public GameObject GetFieldValue(Int32 x, Int32 y)
        {
            return field.GetValue(x, y);
        }

        // TODO doc comment
        public Boolean StartGame()
        {
            if (field is null) throw new TableIsMissingException();

            if (robots is null | robots.Count == 0) throw new RobotsReuiredToPlayException();

            numberOfCurrentRound = 1;

            GameIsActive = true;
            GameIsPaused = false;

            currentRobot = robots[0];
            OnFieldsUpdate();

            return true;
        }

        // TODO doc comment
        public void SetTable(Field field)
        {
            if (GameIsActive) throw new ActiveGameException();

            this.field = field;
        }

        //Temporary
        public void SetRobots(List<Robot> robots)
        {
            if (GameIsActive) throw new ActiveGameException();

            this.robots = robots;
        } 

        /// <summary>
        /// Pauses or resumes the game.
        /// </summary>
        /// <returns>The new state of the game.</returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        public Boolean PauseGame()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();

            GameIsPaused = !GameIsPaused;

            // Additional code here

            return GameIsPaused;
        }

        /// <summary>
        /// Returns the current robot.
        /// </summary>
        /// <returns>The current robot.</returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        public Robot CurrentRobot()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();

            return currentRobot;
        }

        /// <summary>
        /// Advances the current robot to the next one to make it's turn
        /// </summary>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void NextRobot()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            CheckIfAnyTasksIsDone();

            Int32 i = robots.IndexOf(currentRobot);
            if (i == robots.Count - 1)
            {
                WeldCheck();
                currentRobot = robots[0];
                numberOfCurrentRound++;
                taskHandler.ReduceTasksTime();
            }
            else
            {
                currentRobot = robots[i + 1];
            }

            GenerateTasks();

            taskHandler.OnTasksUpdate(currentRobot.team);
            OnFieldsUpdate();
        }

        /// <summary>
        /// Checks where the welding is successful and creates or expends the boxgroups.
        /// </summary>
        public void WeldCheck()
        {
            for(int i=0; i<field.MatrixSize; i++)
            {
                for(int j = 0; j < field.MatrixSize; j++)
                {
                    if(field.GetValue(i,j).GetType() == typeof(Box) && ((Box)field.GetValue(i, j)).attaching != Team.NoTeam)
                    {
                        if(i + 1 < field.MatrixSize && field.GetValue(i + 1, j).GetType() == typeof(Box) && ((Box)field.GetValue(i + 1, j)).attaching != Team.NoTeam)
                        {
                            if(((Box)field.GetValue(i, j)).ingroup == 0 && ((Box)field.GetValue(i + 1, j)).ingroup == 0)
                            {
                                BoxGroup tempboxgroup = new BoxGroup(((Box)field.GetValue(i, j)), ((Box)field.GetValue(i + 1, j)));
                                boxgroups.Add(tempboxgroup.groupid, tempboxgroup);
                            } else if (((Box)field.GetValue(i, j)).ingroup != 0 && ((Box)field.GetValue(i + 1, j)).ingroup == 0)
                            {
                                boxgroups[((Box)field.GetValue(i, j)).ingroup].AddBox((Box)field.GetValue(i, j),(Box)field.GetValue(i + 1, j));
                            } else if(((Box)field.GetValue(i, j)).ingroup == 0 && ((Box)field.GetValue(i + 1, j)).ingroup != 0)
                            {
                                boxgroups[((Box)field.GetValue(i + 1, j)).ingroup].AddBox((Box)field.GetValue(i + 1, j), (Box)field.GetValue(i , j));
                            } else if(((Box)field.GetValue(i, j)).ingroup != 0 && ((Box)field.GetValue(i + 1, j)).ingroup != 0 && ((Box)field.GetValue(i, j)).ingroup != ((Box)field.GetValue(i+1, j)).ingroup)
                            {
                                int delete1 = ((Box)field.GetValue(i, j)).ingroup;
                                int delete2 = ((Box)field.GetValue(i + 1, j)).ingroup;
                                BoxGroup tempboxgroup = new BoxGroup((Box)field.GetValue(i, j), (Box)field.GetValue(i + 1, j), boxgroups[((Box)field.GetValue(i, j)).ingroup], boxgroups[((Box)field.GetValue(i + 1, j)).ingroup]);
                                boxgroups.Remove(delete1);
                                boxgroups.Remove(delete2);
                                boxgroups.Add(tempboxgroup.groupid, tempboxgroup);

                            } else if(((Box)field.GetValue(i, j)).ingroup != 0 && ((Box)field.GetValue(i + 1, j)).ingroup != 0)
                            {
                                boxgroups[((Box)field.GetValue(i, j)).ingroup].AddBox((Box)field.GetValue(i, j), (Box)field.GetValue(i + 1, j));
                            }
                        }
                        if (j + 1 < field.MatrixSize && field.GetValue(i, j + 1).GetType() == typeof(Box) && ((Box)field.GetValue(i, j+1)).attaching != Team.NoTeam)
                        {
                            if (((Box)field.GetValue(i, j)).ingroup == 0 && ((Box)field.GetValue(i, j + 1)).ingroup == 0)
                            {
                                BoxGroup tempboxgroup = new BoxGroup(((Box)field.GetValue(i, j)), ((Box)field.GetValue(i, j + 1)));
                                boxgroups.Add(tempboxgroup.groupid, tempboxgroup);
                            }
                            else if (((Box)field.GetValue(i, j)).ingroup != 0 && ((Box)field.GetValue(i, j + 1)).ingroup == 0)
                            {
                                boxgroups[((Box)field.GetValue(i, j)).ingroup].AddBox((Box)field.GetValue(i, j), (Box)field.GetValue(i, j + 1));
                            }
                            else if (((Box)field.GetValue(i, j)).ingroup == 0 && ((Box)field.GetValue(i, j +1 )).ingroup != 0)
                            {
                                boxgroups[((Box)field.GetValue(i, j +1 )).ingroup].AddBox((Box)field.GetValue(i, j + 1), (Box)field.GetValue(i, j));
                            }
                            else if (((Box)field.GetValue(i, j)).ingroup != 0 && ((Box)field.GetValue(i, j +1)).ingroup != 0 && ((Box)field.GetValue(i, j)).ingroup != ((Box)field.GetValue(i, j + 1)).ingroup)
                            {
                                int delete1 = ((Box)field.GetValue(i, j)).ingroup;
                                int delete2 = ((Box)field.GetValue(i, j + 1)).ingroup;
                                BoxGroup tempboxgroup = new BoxGroup((Box)field.GetValue(i, j), (Box)field.GetValue(i, j + 1), boxgroups[((Box)field.GetValue(i, j)).ingroup], boxgroups[((Box)field.GetValue(i, j + 1)).ingroup]);
                                boxgroups.Remove(delete1);
                                boxgroups.Remove(delete2);
                                boxgroups.Add(tempboxgroup.groupid, tempboxgroup);

                            }
                            else if (((Box)field.GetValue(i, j)).ingroup != 0 && ((Box)field.GetValue(i, j + 1)).ingroup != 0)
                            {
                                boxgroups[((Box)field.GetValue(i, j)).ingroup].AddBox((Box)field.GetValue(i, j), (Box)field.GetValue(i, j + 1));
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < field.MatrixSize; i++)
            {
                for (int j = 0; j < field.MatrixSize; j++)
                {
                    if (field.GetValue(i, j).GetType() == typeof(Box) && ((Box)field.GetValue(i, j)).attaching != Team.NoTeam)
                    {
                        ((Box)field.GetValue(i, j)).attaching = Team.NoTeam;
                    }
                }
            }
        }

        /// <summary>
        /// Makes a turn, changing the current direction of the player, along with any boxes that are connected.
        /// </summary>
        /// <param name="direction">The direction to turn to.</param>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void MakeTurn(Direction direction, Robot r)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            if (!r.IsAttached())
            {
                r.facing = direction;
                r.actionsucces = true;
            }
            else
            {
                int rightmult = 1;
                int leftmult = 1;
                if (r.facing == Direction.Up && direction == Direction.Right
                    || r.facing == Direction.Down && direction == Direction.Left
                    || r.facing == Direction.Right && direction == Direction.Down 
                    || r.facing == Direction.Left && direction == Direction.Up)
                {
                    rightmult= -1;
                }
                else
                {
                    leftmult= -1;
                }
                bool boxesplaceable = true;
                foreach(Robot robot in robots)
                {
                    if(robot.IsAttached() && r.GetAttachedGroupId() == 0)
                    {
                        if ( robot.Attached.id == r.Attached.id  && ((robot.team == r.team && robot.name != r.name) || (robot.team != r.team)))
                        {
                            boxesplaceable = false;
                        }
                    }
                    else if(robot.IsAttached())
                    {
                       if(robot.GetAttachedGroupId() == r.GetAttachedGroupId() && ((robot.team == r.team && robot.name != r.name) || (robot.team != r.team)))
                        {
                            boxesplaceable = false;
                        }
                    }
                    
                }
                if(r.GetAttachedGroupId() != 0)
                {
                    List<Box> boxes = boxgroups[r.GetAttachedGroupId()].boxes;
                    foreach (Box b in boxes)
                    {
                    int newi = (rightmult * (r.j - b.j)) + r.i;
                    int newj = (leftmult * (r.i - b.i)) + r.j;
                    boxesplaceable &= !((newi < 0 || newj < 0) || (field.MatrixSize <= newi || field.MatrixSize <= newj)) && (field.GetValue(newi, newj).IsEmpty() || (field.GetValue(newi, newj).GetType() == typeof(Box) && ((Box)field.GetValue(newi, newj)).ingroup == r.GetAttachedGroupId()));
                    }
                    if (boxesplaceable)
                    {
                        r.actionsucces = true;
                        r.facing = direction;
                        foreach (Box b in boxes)
                        {
                            if (field.GetValue(b.i, b.j).GetType() == typeof(Box) && b.id == ((Box)field.GetValue(b.i, b.j)).id)
                            {
                                field.SetValue(b.i, b.j, new Empty(b.i, b.j));
                            }
                        int newi = (rightmult * (r.j - b.j)) + r.i;
                        int newj = (leftmult * (r.i - b.i)) + r.j;
                        field.SetValue(newi, newj, b);
                        }
                    } else r.actionsucces= false;
                }                                                 
                else
                {
                    Box b = r.Attached;
                    int newi = (rightmult * (r.j - b.j)) + r.i;
                    int newj = (leftmult * (r.i - b.i)) + r.j;
                    boxesplaceable &= !((newi < 0 || newj < 0) || (field.MatrixSize <= newi || field.MatrixSize <= newj)) && field.GetValue(newi, newj).IsEmpty();
                    if (boxesplaceable)
                    {
                        r.actionsucces = true;
                        r.facing = direction;                         
                        field.SetValue(b.i, b.j, new Empty(b.i, b.j));
                        field.SetValue(newi, newj, b);
                    } else r.actionsucces = false;
                }
            }
            OnFieldsUpdate();
        }

        /// <summary>
        /// Moves the player to the given coordinates if it is possible, along with any boxes that it is connected to.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <exception cref="NoActiveGameException"></exception>
        /// <exception cref="GameIsPausedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public void MakeStep(Int32 i, Int32 j, Robot r)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            if (!field.BetweenBorders(i, j))
                return;

            bool robotplaceable = false;
            bool boxesplaceable = true;
            int diffi = i - r.i;
            int diffj = j - r.j;
            try
            {
                robotplaceable = field.GetValue(i, j).IsEmpty();
                if(r.IsAttached() && r.GetAttachedGroupId() != 0)
                {
                    foreach(Robot allrob in robots)
                    {
                        if (allrob.IsAttached() && allrob.GetAttachedGroupId() == r.GetAttachedGroupId() && ((allrob.team == r.team && allrob.name != r.name) || (allrob.team != r.team)))
                        {
                            boxesplaceable= false;
                        }
                    }
                    List<Box> boxes = boxgroups[r.GetAttachedGroupId()].boxes;
                    foreach (Box b in boxes)
                    {
                        boxesplaceable &= ( !((b.i + diffi < 0 || b.j + diffj < 0) || (field.MatrixSize <= b.i + diffi || field.MatrixSize <= b.j + diffj)) 
                            && 
                            ((field.GetValue(b.i + diffi, b.j + diffj).IsEmpty()) 
                            || (field.GetValue(b.i + diffi, b.j + diffj).GetType() == typeof(Box) && ((Box)field.GetValue(b.i + diffi, b.j + diffj)).ingroup == r.GetAttachedGroupId())
                            || (field.GetValue(b.i +diffi ,b.j + diffj).GetType() == typeof(Robot) && ((Robot)field.GetValue(b.i + diffi, b.j + diffj)).team == r.team && ((Robot)field.GetValue(b.i + diffi, b.j + diffj)).name == r.name)
                            ));
                    }
                }
                if(r.IsAttached() && r.GetAttachedGroupId() == 0)
                {
                    foreach (Robot allrob in robots)
                    {
                        if (allrob.IsAttached() && allrob.Attached.id == r.Attached.id && ((allrob.team == r.team && allrob.name != r.name) || (allrob.team != r.team)))
                        {
                            boxesplaceable = false;                   
                        }
                    }
                    Box b = r.Attached;
                    boxesplaceable &= (!((b.i + diffi < 0 || b.j + diffj < 0) || (field.MatrixSize <= b.i + diffi || field.MatrixSize <= b.j + diffj))
                            && (field.GetValue(b.i + diffi, b.j + diffj).IsEmpty()
                        || (field.GetValue(b.i + diffi, b.j + diffj).GetType() == typeof(Robot) && ((Robot)field.GetValue(b.i + diffi, b.j + diffj)).team == r.team && ((Robot)field.GetValue(b.i + diffi, b.j + diffj)).name == r.name)
                       ));
                }
            }
            catch (Exception e)
            { 
                
            }

            if (!((i < 0 || j < 0) || (field.MatrixSize <= i || field.MatrixSize <= j))
                && robotplaceable && !r.IsAttached())
            {
                //set empty at robot old location
                field.SetValue(r.i, r.j, new Empty(r.i, r.j));
                //set robot to the new location
                field.SetValue(i, j, r);
                //tell the robot it's new location
                r.actionsucces = true;
            }
            else if(r.IsAttached() && boxesplaceable && !((i < 0 || j < 0) || (field.MatrixSize <= i || field.MatrixSize <= j)))
            {
                if(r.GetAttachedGroupId() != 0)
                {
                    r.actionsucces = true;
                    List<Box> boxes = boxgroups[r.GetAttachedGroupId()].boxes;
                    foreach (Box b in boxes)
                    {
                        if(field.GetValue(b.i, b.j).GetType() == typeof(Box) && b.id == ((Box)field.GetValue(b.i, b.j)).id)
                        {
                            field.SetValue(b.i, b.j, new Empty(b.i, b.j));
                        }
                        //set robot to the new location
                        field.SetValue(b.i + diffi, b.j + diffj, b);
                        //tell the robot it's new location
                    }
                    if(field.GetValue(r.i, r.j).GetType() == typeof(Robot))
                    {
                        field.SetValue(r.i, r.j, new Empty(r.i, r.j));
                    }
                    //set robot to the new location
                    field.SetValue(i, j, r);
                    //tell the robot it's new location
                } else if(r.GetAttachedGroupId() == 0 && !((i < 0 || j < 0) || (field.MatrixSize <= i || field.MatrixSize <= j)))
                {
                    r.actionsucces = true;
                    Box box = r.Attached;
                    field.SetValue(box.i, box.j , new Empty(box.i, box.j));
                    field.SetValue(box.i + diffi, box.j + diffj, box);
                    
                    if (field.GetValue(r.i, r.j).GetType() == typeof(Robot))
                    {
                        field.SetValue(r.i, r.j, new Empty(r.i, r.j));
                    }                   
                    //set robot to the new location
                    field.SetValue(i, j, r);
                    //tell the robot it's new location
                }
                
            }
            else{
                r.actionsucces = false;
            }
            OnFieldsUpdate();
        }

        /// <summary>
        /// Proceeds to the next round in the game.
        /// </summary>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void Pass()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            // Additional code here

            OnFieldsUpdate();
        }

        /// <summary>
        /// Lowers the health of the box or obstacle infront of the robot and deletes on the field it if it reaches zero.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void Clean(Robot robot)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            //throw new NotImplementedException();

            //Getting the coordinate to be cleaned
            Coordinate cleaning;
            switch(robot.facing)
            {
                case Direction.Up:
                    if (robot.i - 1 < 0)
                    {
                        cleaning = new Coordinate(-1, -1);
                        break;
                    }
                    cleaning = new Coordinate(robot.i - 1, robot.j);
                    break;
                case Direction.Down:
                    if (robot.i + 1 >= field.MatrixSize)
                    {
                        cleaning = new Coordinate(-1, -1);
                        break;
                    }
                    cleaning = new Coordinate(robot.i + 1, robot.j);
                    break;
                case Direction.Left:
                    if (robot.j - 1 < 0)
                    {
                        cleaning = new Coordinate(-1, -1);
                        break;
                    }
                    cleaning = new Coordinate(robot.i, robot.j - 1);
                    break;
                case Direction.Right:
                    if (robot.j + 1 >= field.MatrixSize)
                    {
                        cleaning = new Coordinate(-1, -1);
                        break;
                    }
                    cleaning = new Coordinate(robot.i, robot.j + 1);
                    break;
                default:
                    cleaning = new Coordinate(-1,-1);
                    break;
            }

            if (cleaning.X == -1 ||  cleaning.Y == -1)
                { return;}

            if (field.GetValue(cleaning.X, cleaning.Y) is Obstacle || field.GetValue(cleaning.X, cleaning.Y) is Box && ((Box)field.GetValue(cleaning.X, cleaning.Y)).ingroup == 0)
            {
                robot.actionsucces = true;
                DeletableObject cleingingobject = (DeletableObject)field.GetValue(cleaning.X, cleaning.Y);
                cleingingobject.health--;
                if (cleingingobject.health <= 0)
                {
                    field.SetValue(cleaning.X, cleaning.Y, new Empty(cleaning.X, cleaning.Y));
                }
            }
            else
            {
                robot.actionsucces = false;
            }

            OnFieldsUpdate();
        }

        /// <summary>
        /// Conects the robot to the box in front of it.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void Lift(Robot r)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            r.actionsucces = false;
            if (!r.IsAttached())
            {
                switch (r.facing)
                {
                    case Direction.Up:
                        if (field.GetValue(r.i-1, r.j).GetType() == typeof(Box))
                        {
                            r.Attached = (Box)field.GetValue(r.i - 1, r.j);
                            r.actionsucces = true;
                        }
                        break;
                    case Direction.Down:
                        if (field.GetValue(r.i+1, r.j).GetType() == typeof(Box))
                        {
                            r.Attached = (Box)field.GetValue(r.i+1, r.j);
                            r.actionsucces = true;
                        }
                        break;
                    case Direction.Left:
                        if (field.GetValue(r.i, r.j -1).GetType() == typeof(Box))
                        {
                            r.Attached = (Box)field.GetValue(r.i, r.j-1);
                            r.actionsucces = true;
                        }
                        break;
                    case Direction.Right:
                        if (field.GetValue(r.i , r.j+1).GetType() == typeof(Box))
                        {
                            r.Attached = (Box)field.GetValue(r.i, r.j+1);
                            r.actionsucces = true;
                        }
                        break;
                }
                OnFieldsUpdate();
            }
            else
            {
                r.actionsucces = false;
            }
           
        }

        /// <summary>
        /// Disconects the robot if it is connected to a box.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void UnLift(Robot r)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();
            if (!r.IsAttached())
            {
                r.actionsucces = true;
                r.Attached = null;
                OnFieldsUpdate();
            }
            else
            {
                r.actionsucces = false;
            }
        }

        /// <summary>
        /// Marks a box in front of the robot for welding with the teams color.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void Weld(Robot r)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            switch (r.facing)
            {
                case Direction.Up:
                    if (field.GetValue(r.i - 1, r.j).GetType() == typeof(Box))
                    {
                        ((Box)field.GetValue(r.i - 1, r.j)).attaching = r.team;
                        r.actionsucces = true;
                    }
                    break;
                case Direction.Down:
                    if (field.GetValue(r.i + 1, r.j).GetType() == typeof(Box))
                    {
                        ((Box)field.GetValue(r.i + 1, r.j)).attaching = r.team;
                        r.actionsucces = true;
                    }
                    break;
                case Direction.Left:
                    if (field.GetValue(r.i, r.j - 1).GetType() == typeof(Box))
                    {
                        ((Box)field.GetValue(r.i, r.j - 1)).attaching = r.team;
                        r.actionsucces = true;
                    }
                    break;
                case Direction.Right:
                    if (field.GetValue(r.i, r.j + 1).GetType() == typeof(Box))
                    {
                        ((Box)field.GetValue(r.i, r.j + 1)).attaching = r.team;
                        r.actionsucces = true;
                    }
                    break;
                default:
                    r.actionsucces = false;
                    break;
            }
            OnFieldsUpdate();
        }

        /// <summary>
        /// Disconnects an attachment between the two boxes coordinates after it checked wether they are close to the player and that they are boxes.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void UnWeld(Int32 i1, Int32 j1, Int32 i2, Int32 j2, Robot r)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            Int32 diffi1 = r.i - i1;
            Int32 diffj1 = r.j - j1;
            Int32 diffi2 = r.i - i2;
            Int32 diffj2 = r.j - j2;
            Int32 diffis = i1 - i2;
            Int32 diffjs = j1 - j2;

            bool nexttorobot = (diffi1 <= 1 && diffi1 >= -1 && diffj1 <= 1 && diffj1 >= -1) && (diffi2 <= 1 && diffi2 >= -1 && diffj2 <= 1 && diffj2 >= -1)
                && (diffis <= 1 && diffis >= -1 && diffjs <= 1 && diffjs >= -1) && (i1!= i2 || j1 != j2) && ((Math.Abs(diffis) + (Math.Abs(diffjs))) < 2);

            if ( nexttorobot && field.GetValue(i1, j1).GetType() == typeof(Box) && field.GetValue(i2, j2).GetType() == typeof(Box) && ((Box)field.GetValue(i1, j1)).ingroup != 0 && ((Box)field.GetValue(i2, j2)).ingroup != 0 && ((Box)field.GetValue(i1, j1)).ingroup == ((Box)field.GetValue(i2, j2)).ingroup)
            {
                r.actionsucces = true;
                BoxGroup tempboxgroup = boxgroups[((Box)field.GetValue(i1, j1)).ingroup];
                tempboxgroup.DetachAttachment((Box)field.GetValue(i1, j1), (Box)field.GetValue(i2, j2));
                if (tempboxgroup.CheckGroup() != 0 )
                {
                    boxgroups.Remove(tempboxgroup.groupid);
                }
                else
                {
                    BoxGroup? boxgroupsplit = tempboxgroup.SplitGroup();
                    if(boxgroupsplit != null)
                    {
                        boxgroups.Add(boxgroupsplit.groupid,boxgroupsplit);
                    }
                }
            }
            else
            {
                r.actionsucces = false;
            }
            OnFieldsUpdate();
        }

        public List<Exit> GetExits() { return field.GetExits; }

        public Boolean RobotStandsOnExit()
        {
            foreach(Exit exit in field.GetExits)
            {
                if (currentRobot.i == exit.Coordinate.X &&
                    currentRobot.j == exit.Coordinate.Y &&
                    currentRobot.facing == exit.Direction)
                        return true;
            }

            return false;
        }

        private void CheckIfAnyTasksIsDone()
        {
            if (RobotStandsOnExit() && currentRobot.IsAttached())
            {
                Task? task;
                if (currentRobot.GetAttachedGroupId() == 0)
                {
                    task = taskHandler.GivenPatternIsATaskOfGivenTeam(currentRobot.team, new List<Box>() { currentRobot.Attached });
                }
                else
                {
                    task = taskHandler.GivenPatternIsATaskOfGivenTeam(currentRobot.team, boxgroups[currentRobot.GetAttachedGroupId()].boxes);
                }
                if (task.HasValue)
                {
                    taskHandler.FinishTask(currentRobot.team, task.Value);
                    DeleteBoxGroup(currentRobot.GetAttachedGroupId());
                    currentRobot.Attached = null;
                }
            }
        }

        private void GenerateTasks()
        {
            while (taskHandler.GetTeamTasks(currentRobot.team).Count < TASK_MIN_TASKS_COUNT)
                taskHandler.GenerateRandomTaskForTeam(currentRobot.team);

            if (taskHandler.GetTeamTasks(currentRobot.team).Count >= TASK_MAX_TASKS_COUNT)
                return;

            Random rnd = new Random();
            if (rnd.Next(1, 100) <= TASK_CHANCE_OF_NEW_TASK)
                taskHandler.GenerateRandomTaskForTeam(currentRobot.team);
        }

        /// <summary>
        /// Delets a boxgroup from the field and from the dictionary.
        /// </summary>
        /// <returns></returns>
        private void DeleteBoxGroup(int key)
        {
            if(key != 0)
            {
                BoxGroup dboxgroup = boxgroups[key];
                boxgroups.Remove(key);
                foreach (Box b in dboxgroup.boxes)
                {
                    field.SetValue(b.i, b.j, new Empty(b.i, b.j));
                }
            }
            else
            {
                Box b = currentRobot.Attached;
                field.SetValue(b.i, b.j, new Empty(b.i, b.j));
            }
        }

        /// <summary>
        /// Randomly chooses an event wich can place obstacles or boxes on the map.
        /// </summary>
        /// <returns></returns>
        public void SpecialEvent()
        {
            Random rnd = new Random();
            int x, y;
            x = rnd.Next(field.MatrixSize); y = rnd.Next(field.MatrixSize);                                                                            
            while (!(field.BetweenBorders(x, y)) || !(field.GetValue(x,y).IsEmpty()))
            {
                x = rnd.Next(field.MatrixSize); y = rnd.Next(field.MatrixSize);
            }
            int gameobjectcount = 1;                   
            switch (rnd.Next(3))
            {
                case 0:
                    
                    field.SetValue(x, y, new Obstacle(x, y, 1));
                    if(field.BetweenBorders(x + 1, y) && (field.GetValue(x + 1, y).IsEmpty()))
                    {
                        field.SetValue(x + 1, y, new Obstacle(x + 1, y, 1));
                        gameobjectcount++;
                    }
                    if(field.BetweenBorders(x + 1, y + 1) && (field.GetValue(x + 1, y + 1).IsEmpty()))
                    {
                        field.SetValue(x + 1, y + 1, new Obstacle(x + 1, y + 1, 1));
                        gameobjectcount++;
                    }
                    if (field.BetweenBorders(x, y + 1) && (field.GetValue(x, y + 1).IsEmpty()))
                    {
                        field.SetValue(x, y + 1, new Obstacle(x, y + 1, 1));
                        gameobjectcount++;
                    }
                    SpecialEvents?.Invoke(this, new SpecialEventEventArgs("Meteor", gameobjectcount));
                    break;
                case 1:
                    
                    field.SetValue(x, y, new Obstacle(x, y, 1));
                    for(int i=1; i < 3; i++)
                    {
                        if (field.BetweenBorders(x - i, y) && (field.GetValue(x - i, y).IsEmpty()))
                        {
                            field.SetValue(x - i, y, new Obstacle(x - i, y, 1));
                            gameobjectcount++;
                        }
                    }
                    SpecialEvents?.Invoke(this, new SpecialEventEventArgs("Vertical Wall", gameobjectcount));
                    break;
                case 2:
                    field.SetValue(x, y, new Obstacle(x, y, 1));
                    for (int i = 1; i < 3; i++)
                    {
                        if (field.BetweenBorders(x, y - i) && (field.GetValue(x, y - i).IsEmpty()))
                        {
                            field.SetValue(x, y - i, new Obstacle(x, y - i, 1));
                            gameobjectcount++;
                        }
                    }
                    SpecialEvents?.Invoke(this, new SpecialEventEventArgs("Horizontal Wall", gameobjectcount));
                    break;
            }
            OnFieldsUpdate();
        }

        private void OnFieldsUpdate()
        {
            if (UpdateFields != null)
                UpdateFields(this, new UpdateFieldsEventArgs(field.CalculateVisionOfRobot(currentRobot)));
        }
    }
}
