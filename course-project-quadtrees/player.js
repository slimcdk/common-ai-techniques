class Player {

    constructor(x,y ) {
        this.position = createVector(x, y);
        this.direction = 0;
        this.size = 30;
    }


    show() {
        fill(100,255,0)
        circle(this.position.x, this.position.y, this.size);
        //line(this.position.x, this.position.y, sin(this.position.x) + 20, sin(this.position.y) +20 )
        noFill()
    }

    update() {   
        if (keyIsDown(LEFT_ARROW)) {
            player.rotate(-0.1);
        } else if (keyIsDown(RIGHT_ARROW)) {
            player.rotate(0.1);
        } 
        
        if (keyIsDown(UP_ARROW)) {
            player.move(5);
        } else if (keyIsDown(DOWN_ARROW)) {
            player.move(-5);
        }
    }

    rotate(angle) {
        this.direction += angle;
    }
    
    move(amt) {
        const vel = p5.Vector.fromAngle(this.direction);
        vel.setMag(amt);
        this.position.add(vel);
    }
}

