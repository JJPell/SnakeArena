package main

type UserInput struct {
	up    bool
	down  bool
	left  bool
	right bool
}

func (this UserInput) IsInputDirection() bool {
	if this.up {
		return true
	}
	if this.down {
		return true
	}
	if this.left {
		return true
	}
	if this.right {
		return true
	}
	return false
}
