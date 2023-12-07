using System;
using System.Runtime.InteropServices;
using AppKit;
using CoreFoundation;
using CoreGraphics;

public partial class ProgramTest
{
   public void MainTest()
   {       
            AccessibilityHelper.RequestAccessibilityPermission();

            string bundleIdentifier = "com.apple.Safari";

            NSRunningApplication app = NSRunningApplication.GetRunningApplications(bundleIdentifier)?.First();

            if (app != null)
            {
                int processId = app.ProcessIdentifier;

                IntPtr appElement = AXUIElementCreateApplication((uint)processId);

                IntPtr mainWindow;
            if (AXUIElementCopyAttributeValue(appElement, kAXMainWindowAttribute, out mainWindow) == 0)
            {
                Console.WriteLine("Main Window Handle: " + mainWindow);
            }
            else
            {
                Console.WriteLine("Could not retrieve the main window.");
            }
        }
        else
            {
                Console.WriteLine("Application not found.");
            }
        }

        const string accessibilityFramework = "/System/Library/Frameworks/ApplicationServices.framework/Versions/Current/ApplicationServices";

        [DllImport(accessibilityFramework)]
        static extern IntPtr AXUIElementCreateApplication(uint pid);

        [DllImport(accessibilityFramework)]
        static extern int AXUIElementCopyAttributeValue(IntPtr element, string attribute, out IntPtr value);

        const string kAXMainWindowAttribute = "kAXTitleAttribute";
    }


    public static class AccessibilityHelper
    {
        public static void RequestAccessibilityPermission()
        {
            NSString key = new NSString(AXTrustedCheckOptionPrompt);
            NSDictionary options = NSDictionary.FromObjectAndKey(NSNumber.FromBoolean(true), key);

            IntPtr optionsPtr = options.Handle;
            bool isTrusted = AXIsProcessTrustedWithOptions(optionsPtr);

            if (!isTrusted)
            {
                Console.WriteLine("User denied accessibility permission.");
            }
        }

        const string accessibilityFramework = "/System/Library/Frameworks/ApplicationServices.framework/Versions/Current/ApplicationServices";

        [DllImport(accessibilityFramework)]
        static extern bool AXIsProcessTrustedWithOptions(IntPtr options);

        const string AXTrustedCheckOptionPrompt = "AXTrustedCheckOptionPrompt";
    }