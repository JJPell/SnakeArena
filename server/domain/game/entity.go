package game

type Entity interface {
	GetState() *EntityState
	Update(delta int64, time int64)
}
