grammar Logo;

// Parser rules
// Every identifier must start with a small letter.
// Recursive rules are allowed.
program: commandList;
commandList: command*;
command: forward | turn | repeat | setColor | penUpOrDown;

// The actual LOGO commands.
forward: FORWARD NUMBER;
turn: TURN NUMBER;
repeat: REPEAT NUMBER BLOCKSTART commandList BLOCKEND;
strokeColor: COLOR_BLACK | COLOR_RED | COLOR_GREEN | COLOR_BLUE;
setColor: COLOR strokeColor;
penUpOrDown: PEN (UP | DOWN);

// Lexer rules
// Every identifier must start with a capital letter.
// Rules cannot directly reference each other.
BLOCKSTART: '{';
BLOCKEND: '}';
NUMBER: [0-9]+; // we can use text matching patterns similar to regex
FORWARD: 'forward';
TURN: 'turn';
REPEAT: 'repeat';
COLOR: 'color';

// colors
COLOR_BLACK: 'black';
COLOR_RED: 'red';
COLOR_GREEN: 'green';
COLOR_BLUE: 'blue';

// pen
PEN: 'pen';
UP: 'up';
DOWN: 'down';

// filler characters
WS: [ \t] -> skip; // we're skipping whitespace characters, they will not be part of our syntax tree
NL: [\r\n]+ -> skip;