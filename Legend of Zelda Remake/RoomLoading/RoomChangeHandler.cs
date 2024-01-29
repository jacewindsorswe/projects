using Microsoft.Xna.Framework;
using Sprint0;
using Sprint2;
using Sprint2.Link_Classes;

public class RoomChangeHandler
{
    public Link link1;
    public Dungeon dungeon;
    public int roomTimer;
    bool switchedRoom = false;
    public Game1 currentGame;
    public bool SwitchedRoom
    {
        get { return switchedRoom; }
        set { switchedRoom = value; }
    }
    public RoomChangeHandler(Game1 game, Link link, Dungeon dung, bool switched)
    {
        link1 = link;
        dungeon = dung;
        switchedRoom = switched;
        roomTimer = 0;
        currentGame = game;
    }
    public void Update(GameTime gameTime)
    {
        if (roomTimer > 30 && switchedRoom && !(link1.CurrentXPos <= GlobalUtilities.BOUNDARY_LEFT_X || link1.CurrentXPos >= GlobalUtilities.BOUNDARY_RIGHT_X || link1.CurrentYPos <= GlobalUtilities.BOUNDARY_UP_Y || link1.CurrentYPos >= GlobalUtilities.BOUNDARY_BOTTOM_Y))
        {
            switchedRoom = false;
            roomTimer = 0;
        }
        else
        {
            roomTimer++;
        }
        if ((link1.CurrentXPos <= GlobalUtilities.BOUNDARY_LEFT_X) && !switchedRoom)
        {
            switchedRoom = true;
            dungeon.ChangeRoomLeft();
            LinkInventory.AddRoomToMemory(dungeon.RoomPos[0], dungeon.RoomPos[1]);
            link1.CurrentXPos = ((GlobalUtilities.Resolution[0]) - (link1.CurrentHitbox.Width));
            link1.CurrentYPos = ((((GlobalUtilities.Resolution[1] - GlobalUtilities.HEADS_UP_DISPLAY_SIZE) / 2) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE) - (link1.CurrentHitbox.Height / 2));

        }
        else if ((link1.CurrentXPos >= GlobalUtilities.BOUNDARY_RIGHT_X) && !switchedRoom)
        {
            switchedRoom = true;
            dungeon.ChangeRoomRight();
            LinkInventory.AddRoomToMemory(dungeon.RoomPos[0], dungeon.RoomPos[1]);
            link1.CurrentXPos = (0 + (link1.CurrentHitbox.Width));
            link1.CurrentYPos = ((((GlobalUtilities.Resolution[1] - GlobalUtilities.HEADS_UP_DISPLAY_SIZE) / 2) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE) - (link1.CurrentHitbox.Height / 2));
        }
        else if (!((dungeon.RoomPosX == 1) && (dungeon.RoomPosY == 1)) && (link1.CurrentYPos <= GlobalUtilities.BOUNDARY_UP_Y) && !switchedRoom)
        {
            switchedRoom = true;
            dungeon.ChangeRoomUp();
            LinkInventory.AddRoomToMemory(dungeon.RoomPos[0], dungeon.RoomPos[1]);
            link1.CurrentXPos = ((GlobalUtilities.Resolution[0] / 2) - (link1.CurrentHitbox.Width / 2));
            link1.CurrentYPos = ((GlobalUtilities.Resolution[1]) - (link1.CurrentHitbox.Height / 2));
        }
        else if ((link1.CurrentYPos >= GlobalUtilities.BOUNDARY_BOTTOM_Y) && !switchedRoom)
        {
            switchedRoom = true;
            dungeon.ChangeRoomDown();
            LinkInventory.AddRoomToMemory(dungeon.RoomPos[0], dungeon.RoomPos[1]);
            link1.CurrentXPos = ((GlobalUtilities.Resolution[0] / 2) - (link1.CurrentHitbox.Width / 2));
            link1.CurrentYPos = ((link1.CurrentHitbox.Height / 2) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE);
        }

        if (switchedRoom)
        {
            link1.MakeInvisible();
            if (link1.TimeStopped)
            {
                GlobalUtilities.ResumeTime(link1, currentGame, gameTime);
            }
        }

    }
}