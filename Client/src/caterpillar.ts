export default class Caterpillar extends Phaser.GameObjects.Container {
  private squares: Phaser.GameObjects.Rectangle[] = [];

  private size = 4;

  private squareSize = 16;

  private colour = 0xffffff;

  private previousPosition = {
    x: 0,
    y: 0,
  }

  private squarePositions: Array<{x: number, y: number}> = [];

  constructor(scene: Phaser.Scene, x: number, y: number) {
    super(scene);

    this.previousPosition.x = x;
    this.previousPosition.y = y;

    for (let index = 0; index < this.size; index++) {
      const offset = index * this.squareSize;
      const square = scene.add.rectangle(
        x,
        y - offset,
        this.squareSize - 2,
        this.squareSize - 2,
        this.colour
      );
      this.add(square);
      this.squares.push(square);
      this.squarePositions.push({ x, y: y - (index * this.squareSize) })
    }

    this.scene.add.existing(this);
  }

  changePosition(x: number, y: number) {
    if (!this.hasPositionChanged(x, y)) return;

    this.squares.forEach((square, index) => {
      if (index === 0) {
        square.setPosition(x, y);
        return;
      }

      const position = this.squarePositions[index - 1];
      square.setPosition(position.x, position.y);
    });

    this.squares.forEach((square, index) => {
      this.squarePositions[index].x = square.x;
      this.squarePositions[index].y = square.y;
    });

    this.previousPosition.x = x;
    this.previousPosition.y = y;

    console.log(JSON.stringify(this.squarePositions));
  }

  private hasPositionChanged = (x: number, y: number): boolean => {
    if (x !== this.previousPosition.x) {
      console.log('PositionChanged')
      return true;
    }
    if (y !== this.previousPosition.y) {
      console.log('PositionChanged')
      return true;
    }
    return false;
  }
}
