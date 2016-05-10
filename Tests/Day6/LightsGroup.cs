using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Day6
{
    internal sealed class LightsGroup : ILight
    {
        private readonly IEnumerable<ILight> _lights;

        public LightsGroup(IEnumerable<ILight> lights)
        {
            if (lights == null)
                throw new ArgumentNullException(nameof(lights));

            _lights = lights.Where(light => light != null);
        }

        public int TotalBrightness
        {
            get
            {
                int totalBrightness = _lights.Sum(lights => lights.TotalBrightness);
                return totalBrightness;
            }
        }

        public void Toggle()
        {
            _lights.ForEach(light => light.Toggle());
        }

        public void TurnOff()
        {
            _lights.ForEach(light => light.TurnOff());
        }

        public void TurnOn()
        {
            _lights.ForEach(light => light.TurnOn());
        }
    }
}