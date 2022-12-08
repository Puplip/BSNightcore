using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;





public static class NightcorePatch
{

    private static Harmony harmony;
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> src)
    {
        List<CodeInstruction> ret = new List<CodeInstruction>(src);

        for(int i = 0; i < ret.Count; i++) {

            CodeInstruction inst = ret[i];
            
            if (ret[i].opcode == OpCodes.Ldarg_0 &&
                ret[i+1].opcode == OpCodes.Ldfld &&
                ret[i+2].opcode == OpCodes.Ldc_R4 && (float)ret[i+2].operand == 1.0f &&
                ret[i+3].opcode == OpCodes.Ldloc_S &&
                ret[i+4].opcode == OpCodes.Div)
            {
                ret[i + 3].opcode = OpCodes.Nop;
                ret[i + 4].opcode = OpCodes.Nop;

                break;
            }
        };
        return ret;
    }
    public static void Init()
    {
        harmony = new Harmony("com.puplip.nightcore");
        harmony.Patch(
            original: typeof(GameplayCoreInstaller).GetMethod(nameof(GameplayCoreInstaller.InstallBindings)),
            transpiler: new HarmonyMethod(typeof(NightcorePatch).GetMethod(nameof(NightcorePatch.Transpiler)))
        );
    }
}