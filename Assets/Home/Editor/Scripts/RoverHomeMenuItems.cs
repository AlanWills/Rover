using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace RoverEditor.Home
{
   public static class MenuItems
   {
       [MenuItem(RoverHomeEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadRoverHomeMenuItem()
       {
           LoadSceneSetMenuItem(RoverHomeEditorConstants.SCENE_SET_PATH);
       }
   }
}