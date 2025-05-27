let width,height, side ,grid, gameType, bombs, revealed;
let click=false, hold=false, tileClick=false,tileClickX=0,tileClickY=0;
let r2,r3;
let flag;
function preload(){
    flag=loadImage("/assets/flag.png")
}
function setup() {
    let canvas = createCanvas(600, 600);
    canvas.parent('game-container');
    canvas.elt.addEventListener('contextmenu', e => e.preventDefault());
    if(data.type=="Square"){
        initSquareGrid();
    }
    angleMode(DEGREES);
    r2=Math.sqrt(2);
    r3=Math.sqrt(3);
}
function initSquareGrid(){
    height=data.size;
    width=data.size;
    grid=[];
    for(let i=0;i<height;i++){
        let row=[];
        for(let j=0;j<width;j++){
            let t={bomb:0,revealed:0,known:0,adj:0};
            if(data.bombs[i*width+j]=='1')t.bomb=1;
            if(data.revealed[i*width+j]=='1')t.revealed=1;
            if(data.known[i*width+j]=='1')t.known=1;
            row.push(t);
        }
        grid.push(row);
    }
    side=600/data.size;
    for(let y=0;y<height;y++){
        for(let x=0;x<width;x++){
            if(grid[y][x].bomb){
                for(let dx=-1;dx<=1;dx++){
                    if(x+dx<0||x+dx>=width)continue;
                    for(let dy=-1;dy<=1;dy++){
                        if(y+dy<0||y+dy>=height)continue;
                        if(dx==0&&dy==0)continue;
                        grid[y+dy][x+dx].adj++;
                    }
                }
            }
        }
    }
}
function draw(){
    background(255);
    if(click)click=false;
    if(mouseIsPressed&&!hold){
        hold=true;
        click=true;
    }
    if(!mouseIsPressed)hold=false;
    tileClickX=-1;
    drawGrid();
    if(click&&tileClickX!=-1){
        let t=grid[tileClickY][tileClickX]
        if(mouseButton===LEFT){
            if(!t.flag)t.revealed=1;
        }
        if(mouseButton===RIGHT){
            if(t.flag)t.flag=0;
            else if(!t.revealed)t.flag=1;
        }
    }
}
function drawGrid(){
    if(data.type=="Square"){
        drawSquareGrid();
    }
}
function drawSquareGrid(){
    for(let x=0;x<width;x++){
        for(let y=0;y<height;y++){
            drawTile(x*side+side/2,y*side+side/2,45,4,x,y)
        }
    }

}
function drawTile(x,y,a,n,tileX,tileY){
    let r=side/r2;
    let t=360/n;
    stroke(0);
    for(let i=0;i<n;i++){
        let x1=x+cos(a+i*t)*r,y1=y+sin(a+i*t)*r,x2=x+cos(a+i*t+t)*r,y2=y+sin(a+i*t+t)*r;
        line(x1,y1,x2,y2);
        if(magic(x1,y1,x2,y2,x,y)){
            tileClickX=tileX;
            tileClickY=tileY;
        }
    }
    textAlign(CENTER,CENTER);
    fill(0);
    let tile=grid[tileY][tileX];
    if(tile.revealed){
        textSize(side/2);
        if(tile.bomb)text("B",x,y)
        else if(tile.known)text(tile.adj,x,y)
        else text("?",x,y)
    }
    if(tile.flag){
        imageMode(CENTER);
        image(flag, x, y, side, side);
    }
}

function magic(x1, y1, x2, y2, x3, y3) {
  let denom = (y2 - y3)*(x1 - x3) + (x3 - x2)*(y1 - y3);
  let a = ((y2 - y3)*(mouseX - x3) + (x3 - x2)*(mouseY - y3)) / denom;
  let b = ((y3 - y1)*(mouseX - x3) + (x1 - x3)*(mouseY - y3)) / denom;
  let c = 1 - a - b;
  return a >= 0 && b >= 0 && c >= 0;
}
