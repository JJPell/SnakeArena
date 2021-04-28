import 'phaser';
import { GameObjects } from 'phaser';
import Binary from './Binary';

interface IInput {
    up: boolean;
    down: boolean;
    left: boolean;
    right: boolean;
}

class NetworkService {
    private websocket: WebSocket;
    private lastInputNumber: number;

    constructor() {
        this.websocket = new WebSocket("ws://localhost:8080/ws");
        this.websocket.onmessage = (event) => {
            console.log(event);
        }
    }

    updateInputs(inputs: IInput) {
        const inputsBinary = Object.keys(inputs).map((key) =>  inputs[key] ? 1 : 0)
        const inputsNumber = Binary.toNumber(inputsBinary);

        if (inputsNumber === this.lastInputNumber) return;

        this.lastInputNumber = inputsNumber;
        const data = new Uint8Array(1);
        data[0] = inputsNumber;
        this.websocket.send(data);
    }
}

export default class World extends Phaser.Scene
{
    public rectange: GameObjects.Rectangle;
    private networkService: NetworkService;
    private inputState: IInput = {
        up: false,
        down: false,
        left: false,
        right: false,
    };

    constructor ()
    {
        super('World');
        this.networkService = new NetworkService();


    }

    preload ()
    {
    }

    create ()
    {
        this.rectange = this.add.rectangle(128, 128, 16, 16, 0xffffff);

        this.input.keyboard.on('keydown', (event) => {
            switch (event.key) {
                case "ArrowUp":
                    this.inputState.up = true;
                    break;
                case "ArrowDown":
                    this.inputState.down = true;
                    break;
                case "ArrowLeft":
                    this.inputState.left = true;
                    break;
                case "ArrowRight":
                    this.inputState.right = true;
                    break;     
                default:
                    break;
            }

            this.networkService.updateInputs(this.inputState);
        });

        this.input.keyboard.on('keyup', (event) => {
            switch (event.key) {
                case "ArrowUp":
                    this.inputState.up = false;
                    break;
                case "ArrowDown":
                    this.inputState.down = false;
                    break;
                case "ArrowLeft":
                    this.inputState.left = false;
                    break;
                case "ArrowRight":
                    this.inputState.right = false;
                    break;     
                default:
                    break;
            }

            this.networkService.updateInputs(this.inputState);
        });
    }

    update() {

    }
}

const config = {
    type: Phaser.AUTO,
    backgroundColor: '#125555',
    width: 800,
    height: 600,
    scene: World
};

const game = new Phaser.Game(config);
