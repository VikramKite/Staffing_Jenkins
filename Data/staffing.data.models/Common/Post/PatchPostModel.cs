using System.Collections.Generic;

namespace staffing.data.models.Common.Post
{
    public class PatchPostModel
    {
        public PatchPostModel()
        {
            changed = new Dictionary<string, string>();
        }

        public IDictionary<string, string> changed { get; set; }

        public bool IsValid => (changed != null && changed.Count > 0);
    }
}
