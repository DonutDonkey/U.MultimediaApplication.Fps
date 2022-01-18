namespace Script.So {
    [System.Serializable]
    public class BoolReference {
        public bool UseConstant = true;

        public bool ConstantValue;
        public BoolValue Variable;

        public bool Value {
            get => UseConstant ? ConstantValue : Variable.value;
            set {
                if (UseConstant) 
                    ConstantValue = value;
                else 
                    Variable.value = value;
            }
        }
    }
}