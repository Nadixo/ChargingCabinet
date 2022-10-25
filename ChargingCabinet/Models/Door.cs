using ChargingCabinet.Interfaces;

namespace ChargingCabinet.Models
{
	public class Door : IDoor
	{
		private doorState _state;
		public event EventHandler<DoorEventArgs> CurrentDoorEvent;

		private void NewDoorState(doorState state)
		{
			if (_state != state)
			{
                CurrentDoorEvent?.Invoke(this, new DoorEventArgs { doorState = state });
                _state = state;
			}
		}

		public void onDoorOpen()
		{
			if (_state == doorState.Closed)
				NewDoorState(doorState.Opened);
		}

		public void onDoorClose()
		{
			if (_state == doorState.Opened)
				NewDoorState(doorState.Closed);
		}

		public void lockDoor()
		{
			if (_state == doorState.Closed)
				NewDoorState(doorState.Locked);
			else
				throw new ArgumentException("Door is not closed and therefore cannot be locked");
		}

		public void unlockDoor()
		{
            if (_state == doorState.Locked)
                NewDoorState(doorState.Closed);
            else
                throw new ArgumentException("Door is not locked and therefore cannot be unlocked");
        }
	}
}