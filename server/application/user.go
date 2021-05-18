package application

type UserInput struct {
	Up    bool
	Down  bool
	Left  bool
	Right bool
}

type User struct {
	Id    int
	Input UserInput
}
