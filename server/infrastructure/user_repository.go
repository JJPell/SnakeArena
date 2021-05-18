package infrastructure

import (
	"github.com/JJPell/SnakeArena/domain/user"
)

type UserRepository struct {
	webServer *WebServer
}

func NewUserRepository(webServer *WebServer) *UserRepository {
	return &UserRepository{
		webServer: webServer,
	}
}

func (this *UserRepository) List() []*user.User {
	var users []*user.User

	for id, message := range this.webServer.Messages {
		previousMessage, exists := this.webServer.PreviousMessages[id]

		userState := this.createUserState(message, true, previousMessage, exists)
		userInput := this.createUserInput(message)

		users[id] = &user.User{
			Id:    id,
			State: userState,
			Input: userInput,
		}
	}

	// Add removed users to the list
	removedUsers := this.listRemovedUsers()
	users = append(users, removedUsers...)

	return users
}

func (this *UserRepository) Update(users []*user.User) {
	for i := 0; i < len(users); i++ {
		user := users[i]

		connection, exists := this.webServer.Connections[i]

		if exists {
			connection.WriteJSON(user.GameState)
		}
	}
}

func (this *UserRepository) listRemovedUsers() []*user.User {
	var users []*user.User

	for id, _ := range this.webServer.PreviousMessages {
		_, exists := this.webServer.Messages[id]

		if !exists {
			userStruct := &user.User{
				Id:    id,
				State: user.UserStateRemoved,
				Input: user.UserInput{},
			}

			users = append(users, userStruct)
		}
	}

	return users
}

func (this *UserRepository) createUserState(message byte, messageExists bool, previousMessage byte, previousMessageExists bool) int {
	if messageExists && !previousMessageExists {
		return user.UserStateNew
	}
	if !messageExists && previousMessageExists {
		return user.UserStateRemoved
	}
	if messageExists && previousMessageExists && message != previousMessage {
		return user.UserStateUpdated
	}
	return user.UserStateNone
}

func (this *UserRepository) createUserInput(message byte) user.UserInput {
	input := user.UserInput{
		Up:    false,
		Down:  false,
		Left:  false,
		Right: false,
	}

	index := 0
	for message > 0 {
		inputBool := message%2 == 1

		switch index {
		case 0:
			input.Right = inputBool
		case 1:
			input.Left = inputBool
		case 2:
			input.Down = inputBool
		case 3:
			input.Up = inputBool
		}
		message >>= 1
		index++
	}

	return input
}
