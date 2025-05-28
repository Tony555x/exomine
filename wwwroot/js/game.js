let width,height, side ,grid,altgrid, gameType, bombs, revealed,remaining;
let click=false, hold=false, tileClicked=false;
let r2,r3;
let flag;
function preload(){
    flag=loadImage("/assets/flag.png")
}
function setup() {
    let canvas = createCanvas(600, 600);
    canvas.parent('game-container');
    canvas.elt.addEventListener('contextmenu', e => e.preventDefault());
    r2=Math.sqrt(2);
    r3=Math.sqrt(3);
    if(data.type=="Hexagon"){
        initHexagonGrid();
    }
    if(data.type=="Square"){
        initSquareGrid();
    }
    if(data.type=="Triangle"){
        initTriangleGrid();
    }
    if(data.type=="SquareTriHex"){
        initSquareTriHexGrid();
    }
    remaining=Math.floor(width*height*2/5);
    document.getElementById("rb").innerText=remaining;
    angleMode(DEGREES);
}
function initSquareGrid(){
    height=data.size;
    width=data.size;
    grid=[];
    for(let i=0;i<height;i++){
        let row=[];
        for(let j=0;j<width;j++){
            let t={bomb:0,revealed:0,known:0,tAdj:0,eAdj:0,tBomb:0,rBomb:0,adj:[]};
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
            let t=grid[y][x]
            for(let dx=-1;dx<=1;dx++){
                if(x+dx<0||x+dx>=width)continue;
                for(let dy=-1;dy<=1;dy++){
                    if(y+dy<0||y+dy>=height)continue;
                    if(dx==0&&dy==0)continue;
                    let t2=grid[y+dy][x+dx];
                    t.adj.push(t2);
                    t.tAdj++;
                    if(!t2.revealed)t.eAdj++;
                    if(t2.bomb){
                        t.tBomb++;
                        t.rBomb++;
                    }

                }
            }
        }
    }
}
function initHexagonGrid(){
    height=data.size;
    width=data.size;
    grid=[];
    for(let i=0;i<height;i++){
        let row=[];
        for(let j=0;j<width;j++){
            let t={bomb:0,revealed:0,known:0,tAdj:0,eAdj:0,tBomb:0,rBomb:0,adj:[]};
            if(data.bombs[i*width+j]=='1')t.bomb=1;
            if(data.revealed[i*width+j]=='1')t.revealed=1;
            if(data.known[i*width+j]=='1')t.known=1;
            row.push(t);
        }
        grid.push(row);
    }
    side=600/(data.size+0.5)/r3;
    for(let y=0;y<height;y++){
        for(let x=0;x<width;x++){
            let t=grid[y][x]
            for(let dx=-1;dx<=1;dx++){
                if(x+dx<0||x+dx>=width)continue;
                for(let dy=-1;dy<=1;dy++){
                    if(y+dy<0||y+dy>=height)continue;
                    if(dx==0&&dy==0)continue;
                    if (x%2==0&&dy==1&&dx!=0)continue;
                    if (x%2==1&&dy==-1&&dx!=0)continue;
                    let t2=grid[y+dy][x+dx];
                    t.adj.push(t2);
                    t.tAdj++;
                    if(!t2.revealed)t.eAdj++;
                    if(t2.bomb){
                        t.tBomb++;
                        t.rBomb++;
                    }

                }
            }
        }
    }
}
function initTriangleGrid(){
    height=data.size;
    width=data.size;
    grid=[];
    for(let i=0;i<height;i++){
        let row=[];
        for(let j=0;j<width;j++){
            let t={bomb:0,revealed:0,known:0,tAdj:0,eAdj:0,tBomb:0,rBomb:0,adj:[]};
            if(data.bombs[i*width+j]=='1')t.bomb=1;
            if(data.revealed[i*width+j]=='1')t.revealed=1;
            if(data.known[i*width+j]=='1')t.known=1;
            row.push(t);
        }
        grid.push(row);
    }
    side=600/(data.size*r3/2);
    for(let y=0;y<height;y++){
        for(let x=0;x<width;x++){
            let t=grid[y][x]
            for(let dx=-2;dx<=2;dx++){
                if(x+dx<0||x+dx>=width)continue;
                for(let dy=-1;dy<=1;dy++){
                    if(y+dy<0||y+dy>=height)continue;
                    if(dx==0&&dy==0)continue;
                    if (((x+y)%2)==0&&dy==-1&&abs(dx)>1)continue;
                    if (((x+y)%2)==1&&dy==1&&abs(dx)>1)continue;
                    let t2=grid[y+dy][x+dx];
                    t.adj.push(t2);
                    t.tAdj++;
                    if(!t2.revealed)t.eAdj++;
                    if(t2.bomb){
                        t.tBomb++;
                        t.rBomb++;
                    }

                }
            }
        }
    }

}
function initSquareTriHexGrid(){
    height=data.size+data.size%2-1;
    width=height;
    let offset=height*width;
    grid=[];
    altgrid=[];
    for(let i=0;i<height;i++){
        let row=[];
        for(let j=0;j<width;j++){
            let t={bomb:0,revealed:0,known:0,tAdj:0,eAdj:0,tBomb:0,rBomb:0,adj:[]};
            if(data.bombs[i*width+j]=='1')t.bomb=1;
            if(data.revealed[i*width+j]=='1')t.revealed=1;
            if(data.known[i*width+j]=='1')t.known=1;
            row.push(t);
        }
        grid.push(row);
    }
    for(let i=0;i<height-1;i++){
        let row=[];
        for(let j=0;j<width/2;j++){
            let t={bomb:0,revealed:0,known:0,tAdj:0,eAdj:0,tBomb:0,rBomb:0,adj:[]};
            if(data.bombs[i*width+j+offset]=='1')t.bomb=1;
            if(data.revealed[i*width+j+offset]=='1')t.revealed=1;
            if(data.known[i*width+j+offset]=='1')t.known=1;
            row.push(t);
        }
        altgrid.push(row);
    }
    side=600/(data.size+0.5)/r3;
    for(let x=0;x<width;x++){
        for(let y=0;y<height;y++){
            let t=grid[y][x];
            for(let dx=-1;dx<=1;dx++){
                for(let dy=-1;dy<=1;dy++){
                    if(x%4===0&&dy===1&&dx!==0)continue;
                    if(x%4===2&&dy===-1&&dx!==0)continue;
                    if(x%4===1&&dy===1&&dx>0)continue;
                    if(x%4===1&&dy===-1&&dx<0)continue;
                    if(x%4===3&&dy===1&&dx<0)continue;
                    if(x%4===3&&dy===-1&&dx>0)continue;
                    if(x+dx>=0&&x+dx<width&&y+dy>=0&&y+dy<height&&(dx!==0||dy!==0)){
                        let t2=grid[y+dy][x+dx];
                        t.adj.push(t2);
                        t.tAdj++;
                        if(!t2.revealed)t.eAdj++;
                        if(t2.bomb){
                            t.tBomb++;
                            t.rBomb++;
                        }
                    }
                }
            }
        }
    }

    for(let x=0;x<Math.floor(width/2);x++){
        for(let y=0;y<height-1;y++){
            let v=y-y%2+1;
            let h=x*2+1;
            let t=altgrid[y][x];
            for(let dx=-1;dx<=1;dx++){
                for(let dy=-1;dy<=1;dy++){
                    if(x%2===0&&y%2===0&&dx+dy>0)continue;
                    if(x%2===1&&y%2===0&&-dx+dy>0)continue;
                    if(x%2===0&&y%2===1&&-dx-dy>0)continue;
                    if(x%2===1&&y%2===1&&dx-dy>0)continue;
                    if(h+dx>=0&&h+dx<width&&v+dy>=0&&v+dy<height){
                        let t2=grid[v+dy][h+dx];
                        t.adj.push(t2);
                        t.tAdj++;
                        if(!t2.revealed)t.eAdj++;
                        if(t2.bomb){
                            t.tBomb++;
                            t.rBomb++;
                        }
                        t2.adj.push(t);
                        t2.tAdj++;
                        if(!t.revealed)t2.eAdj++;
                        if(t.bomb){
                            t2.tBomb++;
                            t2.rBomb++;
                        }
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
    tileClicked=null;
    drawGrid();
    if(click&&tileClicked){
        let t=tileClicked;
        if(t.revealed){
            if(t.eAdj>0){
                if(t.rBomb==0||t.rBomb==t.eAdj){
                    for(let i=0;i<t.tAdj;i++){
                        let t2=t.adj[i];
                        if(t2.revealed||t2.flag)continue;
                        if(t.rBomb>0)t2.flag=1;
                        else t2.revealed=1;
                        reveal(t2,t2.bomb,1);
                    }
                }
            }
        }
        if(mouseButton===LEFT){
            if(!t.flag&&!t.revealed){
                t.revealed=1;
                reveal(t,t.bomb,1);
            }
        }
        if(mouseButton===RIGHT){
            if(t.flag){
                t.flag=0;
                reveal(t,1,-1);
            }
            else if(!t.revealed){
                t.flag=1;
                reveal(t,1,1);
            }
        }
    }
}
function reveal(t,v,m){//tile,value,mult(set 1/unset -1)
    remaining-=v*m;
    for(let i=0;i<t.tAdj;i++){
        let t2=t.adj[i];
        t2.eAdj-=m;
        if(v)t2.rBomb-=m;
    }
    document.getElementById("rb").innerText=remaining;
}
function drawGrid(){
    if(data.type=="Hexagon"){
        drawHexagonGrid();
    }
    if(data.type=="Square"){
        drawSquareGrid();
    }
    if(data.type=="Triangle"){
        drawTriangleGrid();
    }
    if(data.type=="SquareTriHex"){
        drawSquareTriHexGrid();
    }
}
function drawHexagonGrid(){
    for(let x=0;x<width;x++){
        for(let y=0;y<height;y++){
            drawTile(x*side*1.5+side,y*side*r3+r3/2*side*(1+x%2),0,6,grid [y][x])
        }
    }

}
function drawSquareGrid(){
    for(let x=0;x<width;x++){
        for(let y=0;y<height;y++){
            drawTile(x*side+side/2,y*side+side/2,45,4,grid[y][x])
        }
    }

}
function drawTriangleGrid(){
    for(let x=0;x<width;x++){
        for(let y=0;y<height;y++){
            drawTile(x*side/2+side/2,y*side*r3/2+side*r3/3-side*r3/6*((x+y)%2),60*((x+y)%2)+30,3,grid[y][x])
        }
    }

}
function drawSquareTriHexGrid(){
    for(let x=0;x<width;x++){
        for(let y=0;y<height;y++){
            let v=0;
            if(x%4)v++;
            if(x%4==2)v++;
            let a=0;
            if(x%2==0&&y%2==1)a=45;
            if(x%2==1){
                if((floor((x%4)/2)+y)%2)a=15;
                else a=75;
            }
            let n=6;
            if(x%2==1||y%2==1)n=4;
            drawTile(
                x*side*(3+r3)/4+side,
                y*side*(r3+1)/2+side*r3/2+(1+r3)*v*side/4,
                a,
                n,
                grid[y][x])
        }
    }

}
function drawTile(x,y,a,n,tile){
    let r=side/2/sin(180/n);
    let t=360/n;
    stroke(0);
    for(let i=0;i<n;i++){
        let x1=x+cos(a+i*t)*r,y1=y+sin(a+i*t)*r,x2=x+cos(a+i*t+t)*r,y2=y+sin(a+i*t+t)*r;
        line(x1,y1,x2,y2);
        if(magic(x1,y1,x2,y2,x,y)){
            tileClicked=tile;
        }
    }
    textAlign(CENTER,CENTER);
    fill(0);
    if(tile.revealed){
        textSize(side*n/8);
        if(tile.bomb)text("B",x,y)
        else if(tile.known)text(tile.tBomb,x,y)
        else text("?",x,y)
    }
    if(tile.flag){
        imageMode(CENTER);
        image(flag, x, y, side*n/4, side*n/4);
    }
}

function magic(x1,y1,x2,y2,x3,y3){
  let denom=(y2-y3)*(x1-x3)+(x3-x2)*(y1-y3);
  let a=((y2-y3)*(mouseX-x3)+(x3-x2)*(mouseY-y3))/denom;
  let b=((y3-y1)*(mouseX-x3)+(x1-x3)*(mouseY-y3))/denom;
  let c=1-a-b;
  return a>=0&&b>=0&&c>=0;
}
