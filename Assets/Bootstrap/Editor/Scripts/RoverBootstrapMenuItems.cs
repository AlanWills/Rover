using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace RoverEditor.Bootstrap
{
   public static class MenuItems
   {
       [MenuItem(RoverBootstrapEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadRoverBootstrapMenuItem()
       {
           LoadSceneSetMenuItem(RoverBootstrapEditorConstants.SCENE_SET_PATH);
       }
   }
}