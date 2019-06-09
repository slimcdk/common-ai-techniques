

let qt;
let player;
let snacks = [];
let method;
let useQuadtree = false;

function setup() {
    console.log("Game starting");
    createCanvas(1000, 600);
    method = createCheckbox('Use quadtree', useQuadtree);
    method.changed(changeMethod);

    player = new Player(random(width * 0.1, width * 0.9), random(height * 0.1, height * 0.9));


    qt = new QuadTree(new Rectangle(width / 2, height / 2, width / 2, height / 2), 4);
    console.log(qt);


    for (let i = 0; i < 2000; i++) {
        let snack = new Snack(random(width), random(height));
        qt.insert(snack);
        snacks.push(snack);
    }
}


function draw() {
    background(255);

    snacks.forEach(snack => snack.show());


    // Find snacks to check for collisions for, using quadtree search or not
    let proximalSnacks = [];
    if (useQuadtree) {

        let range = new Rectangle(player.position.x, player.position.y, player.size*2, player.size*2);
        proximalSnacks = qt.search(range);

        rect(range.x, range.y, range.w * 2, range.h * 2);

        qt.show()
    } else {
        proximalSnacks = snacks;
    }


    // check collisions for nearby snacks
    checks = 0;
    for (i = 0; i < proximalSnacks.length; i++) {

        stroke(200)
        line(player.position.x, player.position.y, proximalSnacks[i].position.x, proximalSnacks[i].position.y);
        /*
        // Euclidiean distance approach
        let dist = player.position.dist(proximalSnacks[i].position);

        if (dist < player.size/2) {
            proximalSnacks.splice(i, 1);
        }
        */
        checks++
    }

    console.log(checks, 'checks performed', frameRate());

    player.show();
    player.update();

    if (mouseIsPressed) {
        let snack = new Snack(mouseX, mouseY);
        qt.insert(snack);
        snacks.push(snack);
    }
}


function changeMethod() {
    useQuadtree = this.checked()
}