using Umbraco.Core;
using Umbraco.Core.Composing;

namespace byte5.EditingLookContentApp.Composer
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class EditingLookComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<EditingLookComponent>();
        }

    }
}