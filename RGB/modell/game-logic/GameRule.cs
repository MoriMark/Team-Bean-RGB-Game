using RGB.modell.boxlogic;
using RGB.modell.enums;
using RGB.modell.events;
using RGB.modell.exceptions;
using RGB.modell.game_logic;
using RGB.modell.gameobjects;
using RGB.modell.structs;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell
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


        public event EventHandler<UpdateFieldsEventArgs> UpdateFields;

        public GameRule()
        {
            GameIsActive = false;
            GameIsPaused = false;
        }

        public GameRule(Int32 numOfRobots, Int32 numOfTeams)
        {
            GameIsActive = false;
            GameIsPaused = false;
            int totalRobots = numOfRobots * numOfTeams;
            int tableSize = totalRobots*4;
            field = new Field(tableSize);
            robots = field.GenerateField(numOfRobots, numOfTeams);
            boxgroups = new Dictionary<int, BoxGroup>();
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
            //throw new NotImplementedException();
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

            Int32 i = robots.IndexOf(currentRobot);
            if (i == robots.Count - 1)
            {
                WeldCheck();
                currentRobot = robots[0];
                numberOfCurrentRound++;
            }
            else
            {
                currentRobot = robots[i + 1];
            }
            OnFieldsUpdate();
        }

        /// <summary>
        /// Checks where the welding is successful
        /// </summary>
        private void WeldCheck()
        {
            for(int i=0; i<field.TableSize; i++)
            {
                for(int j = 0; j < field.TableSize; j++)
                {
                    if(field.GetValue(i,j).GetType() == typeof(Box) && ((Box)field.GetValue(i, j)).attaching != Team.NoTeam)
                    {
                        if(i + 1 < field.TableSize && field.GetValue(i + 1, j).GetType() == typeof(Box) && ((Box)field.GetValue(i + 1, j)).attaching != Team.NoTeam)
                        {
                            if(((Box)field.GetValue(i, j)).ingroup == 0 && ((Box)field.GetValue(i + 1, j)).ingroup == 0)
                            {
                                BoxGroup tempboxgroup = new BoxGroup(((Box)field.GetValue(i, j)), ((Box)field.GetValue(i + 1, j)));
                                boxgroups.Add(tempboxgroup.groupid, tempboxgroup);
                                //((Box)field.GetValue(i, j)).attaching= Team.NoTeam;
                                //((Box)field.GetValue(i + 1, j)).attaching= Team.NoTeam;
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
                        if (j + 1 < field.TableSize && field.GetValue(i, j + 1).GetType() == typeof(Box) && ((Box)field.GetValue(i, j+1)).attaching != Team.NoTeam)
                        {
                            if (((Box)field.GetValue(i, j)).ingroup == 0 && ((Box)field.GetValue(i, j + 1)).ingroup == 0)
                            {
                                BoxGroup tempboxgroup = new BoxGroup(((Box)field.GetValue(i, j)), ((Box)field.GetValue(i, j + 1)));
                                boxgroups.Add(tempboxgroup.groupid, tempboxgroup);
                                //((Box)field.GetValue(i, j)).attaching= Team.NoTeam;
                                //((Box)field.GetValue(i, j)).attaching= Team.NoTeam;
                            }
                            else if (((Box)field.GetValue(i, j)).ingroup != 0 && ((Box)field.GetValue(i, j + 1)).ingroup == 0)
                            {
                                boxgroups[((Box)field.GetValue(i, j)).ingroup].AddBox((Box)field.GetValue(i, j), (Box)field.GetValue(i, j));
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
            for (int i = 0; i < field.TableSize; i++)
            {
                for (int j = 0; j < field.TableSize; j++)
                {
                    if (field.GetValue(i, j).GetType() == typeof(Box) && ((Box)field.GetValue(i, j)).attaching != Team.NoTeam)
                    {
                        ((Box)field.GetValue(i, j)).attaching = Team.NoTeam;
                    }
                }
            }
        }

        /// <summary>
        /// Makes a turn, changing the current direction of the player.
        /// </summary>
        /// <param name="direction">The direction to turn to.</param>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void MakeTurn(Direction direction)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            if (!currentRobot.IsAttached())
            {
                currentRobot.facing = direction;
            }
            else
            {
                if(currentRobot.GetAttachedGroupId() != 0)
                {
                    List<Box> boxes = boxgroups[currentRobot.GetAttachedGroupId()].boxes;
                    if (currentRobot.facing == Direction.Up && direction == Direction.Right
                        || currentRobot.facing == Direction.Down && direction == Direction.Left
                        || currentRobot.facing == Direction.Right && direction == Direction.Up || currentRobot.facing == Direction.Left && direction == Direction.Down)
                    {
                        //right rotation
                        bool boxesplaceable = true;
                        foreach (Box b in boxes)
                        {
                            int diffi = currentRobot.i - b.i;
                            int diffj = -1 * (currentRobot.j - b.j);
                            int diffi2 = -1 * diffj;
                            int diffj2 = diffi;
                            int newi = diffi2 + currentRobot.i;
                            int newj = diffj2 + currentRobot.j;
                            boxesplaceable &= !((newi < 0 || newj < 0) || (field.TableSize <= newi || field.TableSize <= newj)) && (field.GetValue(newi, newj).IsEmpty() || (field.GetValue(newi, newj).GetType() == typeof(Box) && ((Box)field.GetValue(newi, newj)).ingroup == currentRobot.GetAttachedGroupId()));
                        }
                        if (boxesplaceable)
                        {
                            currentRobot.facing = direction;
                            foreach (Box b in boxes)
                            {
                                field.SetValue(b.i, b.j, new Empty(b.i, b.j));
                                int diffi = currentRobot.i - b.i;
                                int diffj = -1 * (currentRobot.j - b.j);
                                int diffi2 = -1 * diffj;
                                int diffj2 = diffi;
                                int newi = diffi2 + currentRobot.i;
                                int newj = diffj2 + currentRobot.j;
                                field.SetValue(newi, newj, b);
                                b.i = newi;
                                b.j = newj;
                            }
                        }

                    }
                    else if (currentRobot.facing == Direction.Down && direction == Direction.Right
                         || currentRobot.facing == Direction.Up && direction == Direction.Left
                        || currentRobot.facing == Direction.Right && direction == Direction.Down || currentRobot.facing == Direction.Left && direction == Direction.Up)
                    {
                        //left rotation
                        bool boxesplaceable = true;
                        foreach (Box b in boxes)
                        {
                            int diffi = currentRobot.i - b.i;
                            int diffj = -1 * (currentRobot.j - b.j);
                            int diffi2 = diffj;
                            int diffj2 = -1 * diffi;
                            int newi = diffi2 + currentRobot.i;
                            int newj = diffj2 + currentRobot.j;
                            boxesplaceable &= !((newi < 0 || newj < 0) || (field.TableSize <= newi || field.TableSize <= newj)) && ((field.GetValue(newi, newj).IsEmpty()) || (field.GetValue(newi, newj).GetType() == typeof(Box) && ((Box)field.GetValue(newi, newj)).ingroup == currentRobot.GetAttachedGroupId()));
                        }
                        if (boxesplaceable)
                        {
                            currentRobot.facing = direction;
                            foreach (Box b in boxes)
                            {
                                field.SetValue(b.i, b.j, new Empty(b.i, b.j));
                                int diffi = currentRobot.i - b.i;
                                int diffj = -1 * (currentRobot.j - b.j);
                                int diffi2 = -1 * diffj;
                                int diffj2 = diffi;
                                int newi = diffi2 + currentRobot.i;
                                int newj = diffj2 + currentRobot.j;
                                field.SetValue(newi, newj, b);
                                b.i = newi;
                                b.j = newj;
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Wrong direction was added relative to player position!");
                    }
                }
                else
                {
                    if ( currentRobot.facing == Direction.Up && direction == Direction.Right
                        || currentRobot.facing == Direction.Down && direction == Direction.Left 
                        || currentRobot.facing == Direction.Right && direction == Direction.Up || currentRobot.facing == Direction.Left && direction == Direction.Down)
                    {
                        //Right Rotation
                        Box b = currentRobot.Attached;
                        bool boxesplaceable = true;
                        int diffi = currentRobot.i - b.i;
                        int diffj = -1 * (currentRobot.j - b.j);
                        int diffi2 = -1 * diffj;
                        int diffj2 = diffi;
                        int newi = diffi2 + currentRobot.i;
                        int newj = diffj2 + currentRobot.j;
                        boxesplaceable = !((newi < 0 || newj < 0) || (field.TableSize <= newi || field.TableSize <= newj)) && field.GetValue(newi, newj).IsEmpty();
                        if (boxesplaceable)
                        {
                            currentRobot.facing = direction;
                           
                            field.SetValue(b.i, b.j, new Empty(b.i, b.j));
                            field.SetValue(newi, newj, b);
                            b.i = newi;
                            b.j = newj;
                            
                        }
                    }
                    else if (currentRobot.facing == Direction.Down && direction == Direction.Right
                         || currentRobot.facing == Direction.Up && direction == Direction.Left
                        || currentRobot.facing == Direction.Right && direction == Direction.Down || currentRobot.facing == Direction.Left && direction == Direction.Up)
                    {
                        //Left Rotation
                        Box b = currentRobot.Attached;
                        bool boxesplaceable = true;
                        int diffi = currentRobot.i - b.i;
                        int diffj = -1 * (currentRobot.j - b.j);
                        int diffi2 = diffj;
                        int diffj2 = -1 * diffi;
                        int newi = diffi2 + currentRobot.i;
                        int newj = diffj2 + currentRobot.j;
                        boxesplaceable = !((newi < 0 || newj < 0) || (field.TableSize <= newi || field.TableSize <= newj)) && field.GetValue(newi, newj).IsEmpty();
                        if (boxesplaceable)
                        {
                            currentRobot.facing = direction;                       
                            field.SetValue(b.i, b.j, new Empty(b.i, b.j));
                            field.SetValue(newi, newj, b);
                            b.i = newi;
                            b.j = newj;
                           
                        }
                    }

                }
                
            }
            

            NextRobot();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <exception cref="NoActiveGameException"></exception>
        /// <exception cref="GameIsPausedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public void MakeStep(Int32 i, Int32 j, Robot robot)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();
            bool robotplaceable = false;
            bool boxesplaceable = true;
            int diffi = i - robot.i;
            int diffj = j - robot.j;
            try
            {
                robotplaceable = field.GetValue(i, j).IsEmpty();
                if(robot.IsAttached() && robot.GetAttachedGroupId() != 0)
                {
                    List<Box> boxes = boxgroups[robot.GetAttachedGroupId()].boxes;
                    foreach (Box b in boxes)
                    {
                        boxesplaceable &= ( !((b.i + diffi < 0 || b.j + diffj < 0) || (field.TableSize <= b.i + diffi || field.TableSize <= b.j + diffj)) 
                            && 
                            ((field.GetValue(b.i + diffi, b.j + diffj).IsEmpty()) 
                            || (field.GetValue(b.i + diffi, b.j + diffj).GetType() == typeof(Box) && ((Box)field.GetValue(b.i + diffi, b.j + diffj)).ingroup == robot.GetAttachedGroupId())
                            || (field.GetValue(b.i +diffi ,b.j + diffj).GetType() == typeof(Robot) && ((Robot)field.GetValue(b.i + diffi, b.j + diffj)).team == robot.team && ((Robot)field.GetValue(b.i + diffi, b.j + diffj)).name == robot.name)
                            ));
                    }
                }
                if(robot.IsAttached() && robot.GetAttachedGroupId() == 0)
                {
                    Box b = robot.Attached;
                    boxesplaceable = (!((b.i + diffi < 0 || b.j + diffj < 0) || (field.TableSize <= b.i + diffi || field.TableSize <= b.j + diffj))
                            && (field.GetValue(b.i + diffi, b.j + diffj).IsEmpty()
                        || (field.GetValue(b.i + diffi, b.j + diffj).GetType() == typeof(Robot) && ((Robot)field.GetValue(b.i + diffi, b.j + diffj)).team == robot.team && ((Robot)field.GetValue(b.i + diffi, b.j + diffj)).name == robot.name)
                       ));
                }

            }
            catch (Exception e)
            { 
                
            }

            if (!((i < 0 || j < 0) || (field.TableSize <= i || field.TableSize <= j))
                && robotplaceable && !robot.IsAttached())
            {
                //set empty at robot old location
                field.SetValue(robot.i, robot.j, new Empty(robot.i, robot.j));
                //set robot to the new location
                field.SetValue(i, j, robot);
                //tell the robot it's new location
                robot.i = i;
                robot.j = j;


            }
            else if(robot.IsAttached() && boxesplaceable && !((i < 0 || j < 0) || (field.TableSize <= i || field.TableSize <= j)))
            {
                if(robot.GetAttachedGroupId() != 0)
                {
                    List<Box> boxes = boxgroups[robot.GetAttachedGroupId()].boxes;

                    foreach (Box b in boxes)
                    {
                        if(field.GetValue(b.i, b.j).GetType() == typeof(Box) && b.id == ((Box)field.GetValue(b.i, b.j)).id)
                        {

                        }
                        field.SetValue(b.i, b.j, new Empty(b.i, b.j));
                        //set robot to the new location
                        field.SetValue(b.i + diffi, b.j + diffj, b);
                        //tell the robot it's new location
                        b.i = b.i + diffi;
                        b.j = b.j + diffj;

                    }
                    if(field.GetValue(robot.i, robot.j).GetType() == typeof(Robot))
                    {
                        field.SetValue(robot.i, robot.j, new Empty(robot.i, robot.j));
                    }
                    //set robot to the new location
                    field.SetValue(i, j, robot);
                    //tell the robot it's new location
                    robot.i = i;
                    robot.j = j;
                } else if(robot.GetAttachedGroupId() == 0 && !((i < 0 || j < 0) || (field.TableSize <= i || field.TableSize <= j)))
                {
                    Box box = robot.Attached;
                    field.SetValue(box.i, box.j , new Empty(box.i, box.j));
                    field.SetValue(box.i + diffi, box.j + diffj, box);
                    box.i = box.i + diffi;
                    box.j = box.j + diffj;
                    
                    if (field.GetValue(robot.i, robot.j).GetType() == typeof(Robot))
                    {
                        field.SetValue(robot.i, robot.j, new Empty(robot.i, robot.j));
                    }
                    
                    //set robot to the new location
                    field.SetValue(i, j, robot);
                    //tell the robot it's new location
                    robot.i = i;
                    robot.j = j;
                }
                
            }

            NextRobot();
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

            NextRobot();
        }

        /// <summary>
        /// 
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
                    if (robot.i + 1 >= field.TableSize)
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
                    if (robot.j + 1 >= field.TableSize)
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

            if (field.GetValue(cleaning.X, cleaning.Y) is Box)
            {
                Box cleaningBox = (Box)field.GetValue(cleaning.X, cleaning.Y);
                cleaningBox.health--;
                if (cleaningBox.health <= 0)
                {
                    field.SetValue(cleaning.X, cleaning.Y, new Empty(cleaning.X, cleaning.Y));
                }
                else
                {
                    field.SetValue(cleaning.X, cleaning.Y, cleaningBox);
                }
            }
            NextRobot();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void Lift()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            if (!currentRobot.IsAttached())
            {
                switch (currentRobot.facing)
                {
                    case Direction.Up:
                        if (field.GetValue(currentRobot.i-1, currentRobot.j).GetType() == typeof(Box))
                        {
                            currentRobot.Attached = (Box)field.GetValue(currentRobot.i - 1, currentRobot.j);
                        }
                        break;
                    case Direction.Down:
                        if (field.GetValue(currentRobot.i+1, currentRobot.j).GetType() == typeof(Box))
                        {
                            currentRobot.Attached = (Box)field.GetValue(currentRobot.i+1, currentRobot.j);
                        }
                        break;
                    case Direction.Left:
                        if (field.GetValue(currentRobot.i, currentRobot.j -1).GetType() == typeof(Box))
                        {
                            currentRobot.Attached = (Box)field.GetValue(currentRobot.i, currentRobot.j-1);
                        }
                        break;
                    case Direction.Right:
                        if (field.GetValue(currentRobot.i , currentRobot.j+1).GetType() == typeof(Box))
                        {
                            currentRobot.Attached = (Box)field.GetValue(currentRobot.i, currentRobot.j+1);
                        }
                        break;
                }
            }
            else
            {
                currentRobot.Attached = null;
            }

            NextRobot();
        }

        public void Weld()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            switch (currentRobot.facing)
            {
                case Direction.Up:
                    if (field.GetValue(currentRobot.i - 1, currentRobot.j).GetType() == typeof(Box))
                    {
                        ((Box)field.GetValue(currentRobot.i - 1, currentRobot.j)).attaching = currentRobot.team;
                    }
                    break;
                case Direction.Down:
                    if (field.GetValue(currentRobot.i + 1, currentRobot.j).GetType() == typeof(Box))
                    {
                        ((Box)field.GetValue(currentRobot.i + 1, currentRobot.j)).attaching = currentRobot.team;
                    }
                    break;
                case Direction.Left:
                    if (field.GetValue(currentRobot.i, currentRobot.j - 1).GetType() == typeof(Box))
                    {
                        ((Box)field.GetValue(currentRobot.i, currentRobot.j - 1)).attaching = currentRobot.team;
                    }
                    break;
                case Direction.Right:
                    if (field.GetValue(currentRobot.i, currentRobot.j + 1).GetType() == typeof(Box))
                    {
                        ((Box)field.GetValue(currentRobot.i, currentRobot.j + 1)).attaching = currentRobot.team;
                    }
                    break;
            }

            NextRobot();
        }

        public void UnWeld(Int32 i1, Int32 j1, Int32 i2, Int32 j2)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            if(field.GetValue(i1, j1).GetType() == typeof(Box) && field.GetValue(i2, j2).GetType() == typeof(Box) && ((Box)field.GetValue(i1, j1)).ingroup != 0 && ((Box)field.GetValue(i2, j2)).ingroup != 0 && ((Box)field.GetValue(i1, j1)).ingroup == ((Box)field.GetValue(i2, j2)).ingroup)
            {
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

            NextRobot();
        }

        private void OnFieldsUpdate()
        {
            if (UpdateFields != null)
                UpdateFields(this, new UpdateFieldsEventArgs(field.CalculateVisionOfRobot(currentRobot)));
        }
    }
}
