class Snack {
    constructor(x,y){
        this.position = createVector(x,y)
        this.x = x;
        this.y = y;
    }

    show () {
        strokeWeight(5)
        point(this.position.x, this.position.y)
        strokeWeight(1)
    }
}

class Rectangle {
    constructor(x, y, w, h) {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }

    contains(node) {
        return (node.position.x >= this.x - this.w &&
            node.position.x < this.x + this.w &&
            node.position.y >= this.y - this.h &&
            node.position.y < this.y + this.h);
    }

    intersects(range) {
        return !(range.x - range.w > this.x + this.w ||
            range.x + range.w < this.x - this.w ||
            range.y - range.h > this.y + this.h ||
            range.y + range.h < this.y - this.h);
    }


}

class QuadTree {
    constructor(boundary, n) {
        this.boundary = boundary;
        this.capacity = n;
        this.nodes = [];
        this.subquads = [];
        this.divided = false;

    }

    subdivide() {
        let x = this.boundary.x;
        let y = this.boundary.y;
        let w = this.boundary.w;
        let h = this.boundary.h;

        this.ne = new QuadTree(new Rectangle(x + w / 2, y - h / 2, w / 2, h / 2), this.capacity);
        this.nw = new QuadTree(new Rectangle(x - w / 2, y - h / 2, w / 2, h / 2), this.capacity);
        this.se = new QuadTree(new Rectangle(x + w / 2, y + h / 2, w / 2, h / 2), this.capacity);
        this.sw = new QuadTree(new Rectangle(x - w / 2, y + h / 2, w / 2, h / 2), this.capacity);
        this.subquads = [this.ne, this.nw, this.se, this.sw];
        this.divided = true;
    }

    insert(node) {

        if (!this.boundary.contains(node)) {
            return false;
        }

        if (this.nodes.length < this.capacity) {
            this.nodes.push(node);
            return true;
        } else {

            if (!this.divided) {
                this.subdivide();
            }

            this.subquads.forEach((qt) => {
                if(qt.insert(node)) {
                    return true 
                }
            });
        }
    }

    search(range, found) {
        if (!found) {
            found = [];
        }
        if (!this.boundary.intersects(range)) {
            return;
        } else {
            for (let n of this.nodes) {
                if (range.contains(n)) {
                    found.push(n);
                }
            }
            if (this.divided) {
                this.nw.search(range, found);
                this.ne.search(range, found);
                this.sw.search(range, found);
                this.se.search(range, found);
            }
        }
        return found;
    }


    show() {
        stroke(150);
        noFill();
        strokeWeight(0.5);
        rectMode(CENTER);
        rect(this.boundary.x, this.boundary.y, this.boundary.w * 2, this.boundary.h * 2);


        for (let n of this.nodes) {
            strokeWeight(2);
            point(n.position.x, n.position.y);
        }

        if (this.divided) {
            this.ne.show();
            this.nw.show();
            this.se.show();
            this.sw.show();
        }
    }





}