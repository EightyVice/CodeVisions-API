/*
    CodeVisions Case Descriptive Language
    Author: Zeyad <8yVice> Ahmed
*/

grammar CDL;

program
    : (casestruct | statement)*
    ;

casestruct
    : 'case' casecond CR ('?' expression CR ':' expression)? CR 
    ;

casecond
    // Input/Output case
    : STRING '>>' STRING                                #CaseInOut
    // An expression that evaluates to a boolean
    | expression                                        #CaseExpression
    ;
statement
    : 'log' '(' expression ')' CR                       #LogStatement
    | expression CR                                     #ExpressionStatement
    | CR                                                #EmptyStatement
    ;

expression
    : primary                                                   #PrimaryExpression
    | identifier                                                #IdentifierExpression
    | identifier '(' expressionList ')'                             #FunctionExpression
    | prefix='!' expression                                     #NotExpression
    | expression bop=('*'|'/'|'%') expression                   #FactorExpression
    | expression bop=('+'|'-') expression                       #TermExpression  
    | expression bop=('<=' | '>=' | '>' | '<') expression       #ComparisonExpression  
    | expression bop=('==' | '!=') expression                   #EqualityExpression  
    | expression bop='and' expression                           #AndExpression
    | expression bop='or' expression                            #OrExpression
    | <assoc=right> expression 
      bop=('=' | '+=' | '-=' | '*=' | '/=' | '%=')
      expression                                                #RightAssocExpression
    ;


primary
    : NUMBER
    | STRING
    | ('true' | 'false')
    ;

identifier
    : IDENTIFIER
    ;

expressionList
    : expression (',' expression)*
    ;
IDENTIFIER: 
    Letter LetterOrDigit*;

fragment LetterOrDigit
    : Letter
    | [0-9]
    ;

fragment Letter
    : [a-zA-Z$_] // these are the "java letters" below 0x7F
    | ~[\u0000-\u007F\uD800-\uDBFF] // covers all characters above 0x7F which are not a surrogate
    | [\uD800-\uDBFF] [\uDC00-\uDFFF] // covers UTF-16 surrogate pairs encodings for U+10000 to U+10FFFF
    ;

NUMBER
    : DECIMAL_LITERAL | FLOAT_LITERAL
    ;

STRING
   : '"' ~ ["\r\n]* '"'
   ;

DECIMAL_LITERAL:    ('0' | [1-9] (Digits? | '_'+ Digits)) [lL]?;

FLOAT_LITERAL:      (Digits '.' Digits? | '.' Digits) [fF]?
             |       Digits [fFdD]?
             ;

fragment Digits
    : [0-9] ([0-9_]* [0-9])?
    ;

CR
   : [\r\n]+
   ;

COMMENT
    : '###' .*? '###' -> skip 
    ;

WS
   : [ \t]+ -> skip
   ;
   
LINE_COMMENT
    : '#' ~[\r\n]* -> skip
    ;