using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace RoverEditor.Startup
{
   public static class MenuItems
   {
       [MenuItem(RoverStartupEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadRoverStartupMenuItem()
       {
           LoadSceneSetMenuItem(RoverStartupEditorConstants.SCENE_SET_PATH);
       }
   }
}