package user

type UserRepository interface {
	List() []*User
	Update(users []*User)
}
