using Content.Server.UserInterface;
using Content.Shared.Access.Components;
using Content.Shared.Economy;
using Content.Shared.Economy.ATM;
using Content.Shared.Store;
using Robust.Server.GameObjects;
using Robust.Shared.Audio;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.Set;

namespace Content.Server.Economy.ATM
{
    [RegisterComponent]
    [ComponentReference(typeof(SharedATMComponent))]
    [Access(typeof(ATMSystem))]
    public sealed class ATMComponent : SharedATMComponent
    {
        [ViewVariables] private BoundUserInterface? UserInterface => Owner.GetUIOrNull(ATMUiKey.Key);
        [ViewVariables(VVAccess.ReadOnly), DataField("currencyWhitelist", customTypeSerializer: typeof(PrototypeIdHashSetSerializer<CurrencyPrototype>))]
        public HashSet<string> CurrencyWhitelist = new();

        [DataField("soundInsertCurrency")]
        // Taken from: https://github.com/Baystation12/Baystation12 at commit 662c08272acd7be79531550919f56f846726eabb
        public SoundSpecifier SoundInsertCurrency = new SoundPathSpecifier("/Audio/_Imperial/Machines/polaroid2.ogg");
        [DataField("soundWithdrawCurrency")]
        // Taken from: https://github.com/Baystation12/Baystation12 at commit 662c08272acd7be79531550919f56f846726eabb
        public SoundSpecifier SoundWithdrawCurrency = new SoundPathSpecifier("/Audio/_Imperial/Machines/polaroid1.ogg");
        [DataField("soundApply")]
        // Taken from: https://github.com/Baystation12/Baystation12 at commit 662c08272acd7be79531550919f56f846726eabb
        public SoundSpecifier SoundApply = new SoundPathSpecifier("/Audio/_Imperial/Machines/chime.ogg");
        [DataField("soundDeny")]
        // Taken from: https://github.com/Baystation12/Baystation12 at commit 662c08272acd7be79531550919f56f846726eabb
        public SoundSpecifier SoundDeny = new SoundPathSpecifier("/Audio/_Imperial/Machines/buzz-sigh.ogg");
        protected override void Initialize()
        {
            base.Initialize();
            Owner.EnsureComponentWarn<ServerUserInterfaceComponent>();
        }
        public void UpdateUserInterface(ATMBoundUserInterfaceState state)
        {
            if (!Initialized || UserInterface == null)
                return;
            UserInterface.SetState(state);
        }
    }
}
