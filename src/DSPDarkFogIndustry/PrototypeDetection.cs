namespace DSPDarkFogIndustry
{
    internal static class PrototypeDetection
    {
        internal static void Disable(AbnormalityProtoSet abnormalities)
        {
            foreach (var abnormality in abnormalities.dataArray)
            {
                // The native factory skips empty names. Keep the category itself so
                // existing saved findings remain readable; other detectors stay active.
                if (abnormality.DeterminatorName == "ABN_ProtoData")
                {
                    abnormality.DeterminatorName = "";
                }
            }
        }
    }
}
