package application

import (
	"github.com/JJPell/SnakeArena/domain/user"
	"github.com/JJPell/SnakeArena/infrastructure"
)

type UserService struct {
	userRepository *infrastructure.UserRepository
}

func NewUserService(userRepository *infrastructure.UserRepository) *UserService {
	return &UserService{
		userRepository: userRepository,
	}
}

func (this *UserService) UpdateUsers(users []*user.User) {
	this.userRepository.Update(users)
}

func (this *UserService) GetUsers() []*user.User {
	return this.userRepository.List()
}
