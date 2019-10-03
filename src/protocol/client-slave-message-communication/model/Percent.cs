namespace client_slave_message_communication.model
{
    public class Percent
    {
        private double _thePercent;
        /// <summary>
        /// If the value given is less than 0 the value will be set to 0,
        /// If the valie is greater than 100.0 the value will be set to 100.0
        /// </summary>
        public double ThePercentage { 
            get => _thePercent;
            set {
                _thePercent = value < 0.0 ? 0.0 : value;
                _thePercent = value > 100.0 ? 100.0 : value;
            }
        }
    }
}
