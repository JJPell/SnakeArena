import 'phaser';
import { Game, GameObjects } from 'phaser';
import Binary from './Binary';
import * as signalR from "@microsoft/signalr";

enum ScreenSize {
    Width = 800,
    Height = 600,
}

interface IInput {
    up: boolean;
    down: boolean;
    left: boolean;
    right: boolean;
}

interface EntityState {
    id: number,
    x: number,
    y: number, 
}

type GameState = EntityState[];

class NetworkService {
    private connection: signalR.HubConnection;
    private lastInputNumber: number;
    private gameState: GameState = [];

    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:44362/game-hub")
        .build();

        this.connection.on("state-update", (json: string) => {
            console.log("State Update");
            console.log(json);
        });
        
        this.connection.start().catch(err => console.error(err)).finally(() => {
            this.connection.send("JoinGame", "Pelly").finally(() => {
                console.log("Sent JoinGame")
                this.connection.send("SendStateUpdate");
            });
        });
    }

    updateInputs = (inputs: IInput) => {

    }
}

export default class World extends Phaser.Scene
{
    public entities: GameObjects.Rectangle[] = [];
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

    createEntity(entityState: EntityState) {
        const entity = this.add.rectangle(entityState.x, entityState.y, 16, 16, 0xffffff);
        this.entities[entityState.id] = entity;
    }

    updateEntity(entityState: EntityState) {
        const entity = this.entities[entityState.id];
        entity.x = (ScreenSize.Width / 2) + (entityState.x * 16);
        entity.y = (ScreenSize.Height / 2) - (entityState.y * 16);
    }

    update() {
        // const gameState = this.networkService.getState();

        // gameState.forEach((entityState) => {
        //     if (this.entities[entityState.id]) {
        //         this.updateEntity(entityState);
        //     } else {
        //         this.createEntity(entityState);
        //     }
        // });
    }
}

const config = {
    type: Phaser.AUTO,
    backgroundColor: '#125555',
    width: ScreenSize.Width,
    height: ScreenSize.Height,
    scene: World
};

const game = new Phaser.Game(config);
