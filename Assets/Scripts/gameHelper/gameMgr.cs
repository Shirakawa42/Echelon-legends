public static class GameHelper {
    public static bool isEndGame(Team[] teams, int teamCount) {
        for (int i = 0; i < teamCount; i++) {
            if (teams[i].hp > 0) {
                return false;
            }
        }

        return true;
    }

    public static void startPreparePhase(Team[] teams, int teamCount) {
    }
}
