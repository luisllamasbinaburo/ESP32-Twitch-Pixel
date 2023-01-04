var canvas = document.getElementById("pixelCanvas");
var ctx = canvas.getContext("2d");

const GRID_COLUMNS = 16;
const GRID_ROWS = 16;
const CELL_WIDTH = 25;
const CELL_HEIGHT = 25;
const GRID_WIDTH = GRID_COLUMNS * CELL_WIDTH;
const GRID_HEIGHT = GRID_ROWS * CELL_HEIGHT

const OFFSET_X = 50;
const OFFSET_Y = 50;
canvas.width = GRID_WIDTH + OFFSET_X;
canvas.height = GRID_HEIGHT + OFFSET_Y;

function drawBoard() {
  ctx.font = "15px Arial";
  ctx.textAlign = "center";
  ctx.strokeStyle = "transparent";
  ctx.fillStyle = "white";
  for (var x = 0; x <= GRID_ROWS; x++) {
    ctx.fillText(x, x * CELL_WIDTH + 60, 45);
  }
  for (var y = 0; y <= GRID_COLUMNS; y++) {
    ctx.fillText(y, 40, y * CELL_HEIGHT + 65);
  }

  ctx.strokeStyle = "#AAAAAA";
  ctx.fillStyle = "#AAAAAA";
  for (var x = 0; x <= GRID_ROWS; x++) {
    ctx.moveTo(x * CELL_WIDTH + OFFSET_X, OFFSET_Y);
    ctx.lineTo(x * CELL_WIDTH + OFFSET_X, GRID_COLUMNS * CELL_HEIGHT + OFFSET_Y);
  }
  for (var y = 0; y <= GRID_COLUMNS; y++) {
    ctx.moveTo(0 + OFFSET_X, y * CELL_WIDTH + OFFSET_Y);
    ctx.lineTo(GRID_ROWS * CELL_HEIGHT + OFFSET_X, y * CELL_WIDTH + OFFSET_Y);
  }
  ctx.stroke();
}

function drawPixel(x, y, color) {
  ctx.fillStyle = color;
  ctx.fillRect(x * CELL_WIDTH + OFFSET_X + 1, y * CELL_HEIGHT + OFFSET_Y + 1, CELL_WIDTH - 2, CELL_HEIGHT - 2);
  ctx.stroke();
}

function clearCanvas() {
  ctx.clearRect(0, 0, canvas.width, canvas.height);
  drawBoard();
}

drawBoard();