namespace ChargingCabinet.Models
{
	public class Door : IDoor
	{
        private doorState;
		public event EventHandler<DoorChangedEventArgs> DoorEvent;

        onDoorOpen()
		{
			if (doorState == doorState.closed)
				newDoorState(doorState.opened);
		}
	}
}