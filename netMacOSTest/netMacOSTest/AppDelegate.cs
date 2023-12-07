using AppKit;


namespace netMacOSTest;

[Register ("AppDelegate")]
public class AppDelegate : NSApplicationDelegate {
	public override void DidFinishLaunching (NSNotification notification)
	{
		ProgramTest program = new ProgramTest();
		program.MainTest();
	}

	public override void WillTerminate (NSNotification notification)
	{
		// Insert code here to tear down your application
	}
}

