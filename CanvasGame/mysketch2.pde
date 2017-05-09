class Rectangle {
    int _x, _y;
    int test = 0;
    
    Rectangle(int x, int y) {
        _x = x;
        _y = y;
    }
    
    void draw() {
        fill(random(100, 255), random(50, 255), random(50, 255));
        noStroke();
        rect(_x, _y, 20, 20);
    }
    
}

class Coordinates {
    int[] _xList1 = new int[20];
    int[] _yList1 = new int[20];
    int[] _xList2 = new int[20];
    int[] _yList2 = new int[20];
    int counter = 0;

    
    Coordinates() {
        for(int i = 0; i < 20; i++) {
            _xList1[i] = -21;
            _yList1[i] = -1;
            _xList2[i] = -1;
            _yList2[i] = -21;
        }
    }
    
    void addCoordinates(int x, int y) {
        _xList1[counter] = x;
        _yList1[counter] = y;
        _xList2[counter] = x + 20;
        _yList2[counter] = y + 20;
        //println(_xList1[counter] + "/" + _xList2[counter] + "/" + _yList1[counter] + "/" + _yList2[counter]);
        counter++;
        
    }
    
    boolean ifCoordinatesIsThere(x, y) {
        boolean flag = false;
    
        
        for(int i = 0; i < 20; i++) {
            if(x >= _xList1[i] && x <= _xList2[i] && y >= _yList1[i] && y <= _yList2[i]) {
                flag = true;
            }
        }
        
        return flag;
    }
}

class MouseCoords {
    int prevX;
    int prevY;
    
    MouseCoords() {
        prevX = 0;
        prevY = 0;
    }
    
    int getPreviousX() {
        return prevX;
    }
    
    int getPreviousY() {
        return prevY;
    }
    
    void addNewXandY(int x, int y) {
        prevX = x;
        prevY = y;
    }
    
}

Rectangle[] rec = new Rectangle[20];
Coordinates cord = new Coordinates();
MouseCoords mcord = new MouseCoords();
int i = 0;
int score = 0;
int counterflag = 0;

PImage img = loadImage("name.png");

void setup() {
    size(800, 600);
    background(51, 51, 51);
    fill(255, 165, 0);
    rect(-1, 550, 801, 50);
    fill(0, 0, 0);
    textSize(20);
    text("Score:", 60, 580);
    text("0pts", 130, 580);
    
    //println("Score: " + score);
    frameRate(1);
    
}

void draw() {
    if(counterflag == 0) {
        image(img, 450, 550, 271, 50);
        counterflag++;
    }
    
    if(second() == 10 || second() == 20 || second() == 30 || second() == 40 || second() == 50 || second == 1) {
        
        if(i < 20) {
            int x = int(random(10, 700));
            int y = int(random(10, 500));
            
            rec[i] = new Rectangle(x, y);
            rec[i].draw();
            cord.addCoordinates(x, y);
            //println(x + "," + y);
        }
    }

}

void mouseClicked() {
    //println(mouseX + "|" + mouseY);
    boolean flag = cord.ifCoordinatesIsThere(mouseX, mouseY);
    
    if(flag) {
        score++;
        //println("Score: " + score);
        
        fill(255, 165, 0);
        rect(120, 560, 60, 40);
        fill(0, 0, 0);
        textSize(20);
        text(score+"pts", 130, 580, (10 + score));
    }
}

void mouseMoved() {

    //Uncomment below to have a white circle follow your cursor
    /*
    int prevX = mcord.getPreviousX();
    int prevY = mcord.getPreviousY();
        
    fill(51, 51, 51);
    noStroke();
    ellipse(prevX, prevY, 12, 12);
    
    fill(255, 255, 255);
    noStroke();
    ellipse(mouseX, mouseY, 10, 10);
    
    mcord.addNewXandY(mouseX, mouseY);
    */
}