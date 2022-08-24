namespace SwecorTestSavchenko
{
    public class GameEvent
    {
        public const string ENTITY_CREATED = "entity_created";

        public const string START_GAME = "start_game";

        public const string SHOT_MADE = "shot_made";
        public const string CREATE_ENEMY_SHIP = "create_enemy_ship";
        public const string CREATE_PLAYER_BASE = "create_player_base";

        public const string SHELL_HITTED_ENEMY_SHIP = "shell_hitted_enemy_ship";
        public const string COLLISIONS_CHECKED = "collisions_checked";

        public const string MOVEMENT_DONE = "movement_done";

        public const string OUT_OF_SCREEN_CHECKED = "out_of_screen_checked";
        
        public const string POINTER_DOWN = "pointer_down";
        public const string POINTER_UP = "pointer_up";

        public const string DAMAGE_DEALED = "damage_dealed";

        public const string ADD_ENTITY_TO_REMOVE_LIST = "add_entity_to_remove_list";
        public const string REMOVE_ENTITY = "remove_entity";

        public const string CHANGE_ACTIVE_GUN = "change_active_gun";
    }
}