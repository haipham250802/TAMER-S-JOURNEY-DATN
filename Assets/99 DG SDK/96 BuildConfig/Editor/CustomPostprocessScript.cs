#if UNITY_IOS
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class CustomPostprocessScript : ScriptableObject
{
    public DefaultAsset m_entitlementsFile;

    public DefaultAsset m_entitlementsFilePre;

    [PostProcessBuild]
    public static void ChangeXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            // Get plist
            var plistPath = pathToBuiltProject + "/Info.plist";
            var plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            // Get root
            var rootDict = plist.root;

            // add key
            rootDict.SetString("NSLocationAlwaysUsageDescription", "Location is required to find out where you are");
            rootDict.SetString("NSLocationWhenInUseUsageDescription", "Location is required to find out where you are");
            rootDict.SetString("NSCalendarsUsageDescription", "Share score your friends");
            rootDict.SetString("NSUserTrackingUsageDescription", "Your data will be used to deliver personalized ads to you.");
            rootDict.SetString("NSAdvertisingAttributionReportEndpoint", "https://appsflyer-skadnetwork.com/");
            PlistElementArray urlTypesArray = rootDict.CreateArray("SKAdNetworkItems");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "22mmun2rn5.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "238da6jt44.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "424m5254lk.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "4468km3ulz.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "44jx6755aq.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "44n7hlldy6.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "488r3q3dtq.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "4dzt52r2t5.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "4fzdc2evr5.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "4pfyvq9l8r.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "523jb4fst2.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "52fl2v3hgk.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "578prtvx9j.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "5a6flpkh64.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "5l3tpt7t6e.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "5lm9lj6jb7.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "737z793b9f.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "7953jerfzd.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "7rz58n8ntl.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "7ug5zh24hu.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "8s468mfl3y.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "97r2b46745.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "24t9a8vw3c.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "252b5q8x7y.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "5tjdwbrq8w.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "2u9pt9hc89.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "3qy4746246.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "ppxm28t8ap.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "s39g8k73mm.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "su67r6k2v3.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "3rd42ekr43.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "3sh42y64q3.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "9g2aggbj52.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "9rd848q2bz.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "9t245vhmpl.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "9yg77x724h.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "av6w8kgt66.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "bvpn9ufa9b.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "c6k4g5qg8m.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "cg4yq2srnc.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "cj5566h2ga.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "cstr6suwn9.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "dzg6xy7pwj.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "wg4vff78zm.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "wzmmz9fp6w.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "xy9t38ct57.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "y45688jllp.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "yclnxrl5pm.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "ydx93a7ass.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "yrqqpx2mcb.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "z4gj7hsk7h.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "zmvfpc5aq8.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "ecpz2srf59.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "ejvt5qm6ak.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "f38h382jlk.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "f73kdq92p3.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "g28c52eehv.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "ggvn48r87g.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "glqzh8vgby.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "gta9lk7p23.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "gvmwg8q7h5.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "hdw39hrw9y.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "hs6bdukanm.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "kbd757ywx3.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "klf5c3l5u5.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "lr83yxwka7.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "ludvb6z3bs.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "m8dbw4sv7c.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "mlmmfzh3r3.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "mls7yz5dvl.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "mtkv5xtk9e.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "n38lu8286q.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "n66cz3y3bx.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "n9x2a789qt.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "nzq8sh4pbs.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "p78axxw29g.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "t38b2kh725.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "tl55sbb4fm.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "u679fj5vs4.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "uw77j35x4d.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "v72qych5uu.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "v79kvwwj4g.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "v9wttpbfk9.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "w9q455wk68.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "zq492l623r.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "x44k69ngh6.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "k674qkevps.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "prcb7njmu6.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "mp6xlyr22a.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "32z4fx6l9h.skadnetwork");
            AddArrayPlist(urlTypesArray, "SKAdNetworkIdentifier", "f7s53z58qe.skadnetwork");
            var buildKey = "UIBackgroundModes";
            rootDict.CreateArray(buildKey).AddString("remote-notification");
            if (rootDict.values.ContainsKey("UIApplicationExitsOnSuspend"))
            {
                rootDict.values.Remove("UIApplicationExitsOnSuspend");
            }
            rootDict.SetString("NSCameraUsageDescription", "not using the camera");
            rootDict.SetString("NSMicrophoneUsageDescription", "not using the microphone");
            rootDict.SetString("NSLocationUsageDescription", "not using the location");
            //write http protoco
            PlistElementDict allowsDict = plist.root.CreateDict("NSAppTransportSecurity");

            allowsDict.SetBoolean("NSAllowsArbitraryLoads", true);
            allowsDict.SetBoolean("NSAllowsArbitraryLoadsInWebContent", true);

            var exceptionsDict = allowsDict.CreateDict("NSExceptionDomains");

            var domainDict = exceptionsDict.CreateDict("ip-api.com");
            var domainDictRocket = exceptionsDict.CreateDict("rocketstudio.com.vn");
            //var domainDictHerokuapp = exceptionsDict.CreateDict("space-shooter-server-colyseus.herokuapp.com");
            var domainDictLoveMoney = exceptionsDict.CreateDict("lovemoney.vn");
            var domainDictIP = exceptionsDict.CreateDict("127.0.0.1");
            var domainDictCountryFlag = exceptionsDict.CreateDict("countryflags.io");

            domainDict.SetBoolean("NSExceptionAllowsInsecureHTTPLoads", true);
            domainDict.SetBoolean("NSIncludesSubdomains", true);

            domainDictRocket.SetBoolean("NSExceptionAllowsInsecureHTTPLoads", true);
            domainDictRocket.SetBoolean("NSIncludesSubdomains", true);

            domainDictLoveMoney.SetBoolean("NSExceptionAllowsInsecureHTTPLoads", true);
            domainDictLoveMoney.SetBoolean("NSIncludesSubdomains", true);

            domainDictIP.SetBoolean("NSExceptionAllowsInsecureHTTPLoads", true);
            domainDictIP.SetBoolean("NSIncludesSubdomains", true);

            domainDictCountryFlag.SetBoolean("NSExceptionAllowsInsecureHTTPLoads", true);
            domainDictCountryFlag.SetBoolean("NSIncludesSubdomains", true);

            //domainDictHerokuapp.SetBoolean("NSExceptionAllowsInsecureHTTPLoads", true);
            //domainDictHerokuapp.SetBoolean("NSIncludesSubdomains", true);
          

            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }

    public static void AddArrayPlist(PlistElementArray urlTypesArray, string key, string value)
    {
        PlistElementDict dict = urlTypesArray.AddDict();
        dict.SetString(key, value);
    }

    [PostProcessBuild]
    public static void OnPostProcessAssociedDomains(BuildTarget buildTarget, string buildPath)
    {
        if (buildTarget != BuildTarget.iOS) return;

        var dummy = CreateInstance<CustomPostprocessScript>();
        var file = dummy.m_entitlementsFile;
#if PREMIUM_VERSION
        file = dummy.m_entitlementsFilePre;
#endif

        DestroyImmediate(dummy);
        if (file == null) return;

        var proj_path = PBXProject.GetPBXProjectPath(buildPath);
        var proj = new PBXProject();
        proj.ReadFromFile(proj_path);

        // target_name = "Unity-iPhone"
        var target_name = "Unity-iPhone";//proj.GetUnityMainTargetGuid();
        var target_guid = proj.TargetGuidByName(target_name);
        var src = AssetDatabase.GetAssetPath(file);
        var file_name = Path.GetFileName(src);
        var dst = buildPath + "/" + target_name + "/" + file_name;
        FileUtil.CopyFileOrDirectory(src, dst);
        proj.AddFile(target_name + "/" + file_name, file_name);
        proj.AddBuildProperty(target_guid, "CODE_SIGN_ENTITLEMENTS", target_name + "/" + file_name);
        proj.WriteToFile(proj_path);
    }

    [PostProcessBuild(1)]
    public static void OnPostProcessBuildFramework(BuildTarget target, string path)
    {
        if (target == BuildTarget.iOS)
        {
            // Read.
            var projectPath = PBXProject.GetPBXProjectPath(path);
            var project = new PBXProject();
            project.ReadFromFile(projectPath);
            //var targetName = "Unity-iPhone";//PBXProject.GetUnityTargetName(); // note, not "project." ...
            var targetGUID = project.GetUnityMainTargetGuid();
            project.AddCapability(targetGUID, PBXCapabilityType.InAppPurchase);
            project.AddCapability(targetGUID, PBXCapabilityType.PushNotifications);
            AddFrameworks(project, targetGUID);

            project.AddCapability(targetGUID, PBXCapabilityType.SignInWithApple);
            // Write.
            File.WriteAllText(projectPath, project.WriteToString());
        }
    }

    [PostProcessBuild(999)]
    public static void OnPostProcessBuildDisableBitCode(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            var projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
            var pbxProject = new PBXProject();
            pbxProject.ReadFromFile(projectPath);
            var target = pbxProject.GetUnityMainTargetGuid();
            pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
            pbxProject.SetBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");

            pbxProject.WriteToFile(projectPath);
        }
    }

    [PostProcessBuild(9999)]
    public static void OnPostProcessBuildLocalize(BuildTarget target, string path)
    {
        if (target == BuildTarget.iOS) OnIOSBuildCopyLocalize(target, path);
    }

    private static void OnIOSBuildCopyLocalize(BuildTarget target, string path)
    {
        //var parentDirectory = Directory.GetParent(Application.dataPath);
        //var localizePath = Path.Combine(parentDirectory.FullName, "XCodeLocalize");
        //NativeLocale.AddLocalizedStringsIOS(path, localizePath);
    }

    private static void AddFrameworks(PBXProject project, string targetGUID)
    {

        project.AddFrameworkToProject(targetGUID, "UserNotificationsUI.framework", true);
        project.AddFrameworkToProject(targetGUID, "UserNotifications.framework", true);
        project.AddFrameworkToProject(targetGUID, "iAd.framework", true);
        project.AddFrameworkToProject(targetGUID, "StoreKit.framework", true);
        project.AddFrameworkToProject(targetGUID, "AuthenticationServices.framework", true);

    }
}
#endif