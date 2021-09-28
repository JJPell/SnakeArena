export default class Food extends Phaser.GameObjects.Container {
  private square: Phaser.GameObjects.Rectangle;

  private squareSize = 16;

  private colour = 0xffffff;

  constructor(scene: Phaser.Scene, x: number, y: number) {
    super(scene);

    this.square = scene.add.rectangle(
      x,
      y,
      this.squareSize - 2,
      this.squareSize - 2,
      this.colour
    );

    this.add(this.square);

    this.scene.add.existing(this);
  }
}
