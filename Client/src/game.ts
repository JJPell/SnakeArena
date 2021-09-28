import 'phaser';
import { Game, GameObjects } from 'phaser';
import Binary from './Binary';
import * as signalR from "@microsoft/signalr";
import Caterpillar from './caterpillar';
import Food from './food';

enum ScreenSize {
    Width = 800,
    Height = 600,
}

enum EntityType {
    Player,
    Food,
}

interface IInput {
    up: boolean;
    down: boolean;
    left: boolean;
    right: boolean;
}

interface IEntityState {
    id: number,
    type: EntityType,
    x: number,
    y: number, 
}

interface IPlayerState extends IEntityState {
    name: string,
}

type IGameState = IEntityState[];

class NetworkService {
    private connection: signalR.HubConnection;
    private lastInputNumber: number;
    private gameState: IGameState = [];
    private previousGameState: IGameState = [];
    private removedEntities: number[] = []

    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:44362/game-hub")
        .build();

        this.connection.on("state-update", (json: string) => {
            console.log("State Update");
            console.log(json);
            this.previousGameState = this.gameState;
            const state = JSON.parse(json);
            this.gameState = state.map((rawEntity) => {
                const player: IPlayerState = {
                    id: rawEntity.Id,
                    type: rawEntity.Type,
                    x: rawEntity.X,
                    y: rawEntity.Y,
                    name: rawEntity.Name,
                }

                return player;
            });

            this.previousGameState.forEach((previousEntity) => {
                for (let index = 0; index < this.gameState.length; index++) {
                    const entity = this.gameState[index];
                    
                    if (entity.id === previousEntity.id) {
                        return;
                    }
                }

                this.removedEntities.push(previousEntity.id);
            });
        });
        
        this.connection.start().catch(err => console.error(err)).finally(() => {
            this.connection.send("JoinGame", "Pelly").finally(() => {
                console.log("Sent JoinGame");
            });
        });
    }

    getState = (): IGameState => this.gameState;

    getRemovedEntities = (): number[] => {
        const removedEntities = JSON.parse(JSON.stringify(this.removedEntities));
        this.removedEntities = [];
        return removedEntities;
    };

    updateInputs = (inputs: IInput) => {
        const inputNumber = Binary.toNumber([inputs.left, inputs.right, inputs.up, inputs.down]);
        this.connection.send('Input', inputNumber);
    }
}

export default class World extends Phaser.Scene
{
    public entities: Record<number, Caterpillar | Food> = {};
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

    createEntity(entityState: IEntityState) {
        switch (entityState.type) {
            case EntityType.Player:
                this.createPlayer(entityState);
                break;
            case EntityType.Food:
                this.createFood(entityState);
                break;
            default:
                break;
        }
    }

    createFood(entityState: IEntityState) {
        const x = (ScreenSize.Width / 2) + (entityState.x * 16);
        const y = (ScreenSize.Height / 2) - (entityState.y * 16);
        const entity = new Food(this, x, y);
        this.entities[entityState.id] = entity;
    }

    createPlayer(entityState: IEntityState) {
        const x = (ScreenSize.Width / 2) + (entityState.x * 16);
        const y = (ScreenSize.Height / 2) - (entityState.y * 16);
        const entity = new Caterpillar(this, x, y);
        this.entities[entityState.id] = entity;
    }

    updateEntity(entityState: IEntityState) {
        switch (entityState.type) {
            case EntityType.Player:
                this.updatePlayer(entityState);
                break;
            default:
                break;
        }
    }

    updatePlayer(entityState: IEntityState) {
        const entity = this.entities[entityState.id] as Caterpillar;
        const x = (ScreenSize.Width / 2) + (entityState.x * 16);
        const y = (ScreenSize.Height / 2) - (entityState.y * 16);
        entity.changePosition(x, y);
    }

    update() {
        const gameState = this.networkService.getState();

        gameState.forEach((entityState) => {
            if (this.entities[entityState.id]) {
                this.updateEntity(entityState);
            } else {
                this.createEntity(entityState);
            }
        });

        const removedEntities = this.networkService.getRemovedEntities();
        removedEntities.forEach((id) => {
            this.entities[id].destroy();
            delete this.entities[id];
        })

        Object.values(this.entities).forEach((entity) => entity.update());
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
