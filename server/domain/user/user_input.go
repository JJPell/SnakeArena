package user

type UserInput struct {
	Up    bool
	Down  bool
	Left  bool
	Right bool
}

func (this *UserInput) IsInput() bool {
	if this.Up {
		return true
	}
	if this.Down {
		return true
	}
	if this.Left {
		return true
	}
	if this.Right {
		return true
	}
	return false
}
