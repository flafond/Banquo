namespace Banquo.Extensions.BrowserStack
{
    public static class BrowserStack
    {
        // FIXME: for some reason this isn't working...
        public static void BrowserStackStatus(this User user, bool passed, string msg)
        {
            object[] args = new object[]
            {
                passed ? "passed" : "failed",
                msg
            };

            user.ExecuteScript(
                "browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"arguments[0]\", \"reason\": \"arguments[1]\"}}",
                args
            );
        }

        // Once the above is working, use this
        /// public static void BrowserStackPassed(this User user, string msg) =>
        ///     user.BrowserStackStatus(true, msg);
        public static void BrowserStackPassed(this User user, string msg)
        {
            user.ExecuteScript(
                "browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \""+msg+"\"}}",
                default
            );
        }

        // Once the above is working, use this
        /// public static void BrowserStackPassed(this User user, string msg) =>
        ///     user.BrowserStackStatus(false, msg);
        public static void BrowserStackFailed(this User user, string msg)
        {
            user.ExecuteScript(
                "browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + msg + "\"}}",
                default
            );
        }
    }
}