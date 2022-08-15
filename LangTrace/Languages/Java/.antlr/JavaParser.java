// Generated from c:\Users\8yvic\source\repos\CodeVisions\LangTrace\Languages\Java\Java.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class JavaParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, T__40=41, T__41=42, T__42=43, T__43=44, T__44=45, 
		T__45=46, T__46=47, T__47=48, T__48=49, T__49=50, T__50=51, T__51=52, 
		T__52=53, T__53=54, T__54=55, T__55=56, T__56=57, T__57=58, DECIMAL_LITERAL=59, 
		HEX_LITERAL=60, OCT_LITERAL=61, BINARY_LITERAL=62, FLOAT_LITERAL=63, HEX_FLOAT_LITERAL=64, 
		BOOL_LITERAL=65, CHAR_LITERAL=66, STRING_LITERAL=67, IDENTIFIER=68;
	public static final int
		RULE_prog = 0, RULE_init = 1, RULE_declaration = 2, RULE_linkedList = 3, 
		RULE_primitive = 4, RULE_memberDecl = 5, RULE_classDec = 6, RULE_type = 7, 
		RULE_funcCall = 8, RULE_primitiveTypes = 9, RULE_arithType = 10, RULE_arithTypeNOINT = 11, 
		RULE_signedorunsigned = 12, RULE_declarators = 13, RULE_declarator = 14, 
		RULE_declId = 15, RULE_arrBracket = 16, RULE_pointer = 17, RULE_expression = 18, 
		RULE_initializer = 19, RULE_arrayInit = 20, RULE_statement = 21, RULE_exprpar = 22, 
		RULE_primary = 23, RULE_identifier = 24, RULE_literal = 25, RULE_integerLiteral = 26, 
		RULE_floatLiteral = 27, RULE_expressionList = 28;
	private static String[] makeRuleNames() {
		return new String[] {
			"prog", "init", "declaration", "linkedList", "primitive", "memberDecl", 
			"classDec", "type", "funcCall", "primitiveTypes", "arithType", "arithTypeNOINT", 
			"signedorunsigned", "declarators", "declarator", "declId", "arrBracket", 
			"pointer", "expression", "initializer", "arrayInit", "statement", "exprpar", 
			"primary", "identifier", "literal", "integerLiteral", "floatLiteral", 
			"expressionList"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'List'", "'<'", "'>'", "'='", "';'", "'class'", "'{'", "'}'", 
			"'('", "')'", "'char'", "'short'", "'int'", "'long'", "'float'", "'double'", 
			"'signed'", "'unsgined'", "','", "'['", "']'", "'*'", "'.'", "'new'", 
			"'++'", "'--'", "'+'", "'-'", "'~'", "'!'", "'&'", "'/'", "'%'", "'<='", 
			"'>='", "'=='", "'!='", "'^'", "'|'", "'&&'", "'||'", "'?'", "':'", "'+='", 
			"'-='", "'*='", "'/='", "'&='", "'|='", "'^='", "'>>='", "'<<='", "'%='", 
			"'if'", "'else'", "'while'", "'do'", "'return'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, "DECIMAL_LITERAL", 
			"HEX_LITERAL", "OCT_LITERAL", "BINARY_LITERAL", "FLOAT_LITERAL", "HEX_FLOAT_LITERAL", 
			"BOOL_LITERAL", "CHAR_LITERAL", "STRING_LITERAL", "IDENTIFIER"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Java.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public JavaParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ProgContext extends ParserRuleContext {
		public List<InitContext> init() {
			return getRuleContexts(InitContext.class);
		}
		public InitContext init(int i) {
			return getRuleContext(InitContext.class,i);
		}
		public ProgContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_prog; }
	}

	public final ProgContext prog() throws RecognitionException {
		ProgContext _localctx = new ProgContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_prog);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(59); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(58);
				init();
				}
				}
				setState(61); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__4) | (1L << T__5) | (1L << T__8) | (1L << T__10) | (1L << T__11) | (1L << T__12) | (1L << T__13) | (1L << T__14) | (1L << T__15) | (1L << T__16) | (1L << T__17) | (1L << T__21) | (1L << T__23) | (1L << T__24) | (1L << T__25) | (1L << T__26) | (1L << T__27) | (1L << T__28) | (1L << T__29) | (1L << T__30) | (1L << T__53) | (1L << T__55) | (1L << T__56) | (1L << T__57) | (1L << DECIMAL_LITERAL) | (1L << HEX_LITERAL) | (1L << OCT_LITERAL) | (1L << BINARY_LITERAL) | (1L << FLOAT_LITERAL))) != 0) || _la==HEX_FLOAT_LITERAL || _la==IDENTIFIER );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InitContext extends ParserRuleContext {
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public DeclarationContext declaration() {
			return getRuleContext(DeclarationContext.class,0);
		}
		public InitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_init; }
	}

	public final InitContext init() throws RecognitionException {
		InitContext _localctx = new InitContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_init);
		try {
			setState(65);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,1,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(63);
				statement();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(64);
				declaration();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclarationContext extends ParserRuleContext {
		public DeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declaration; }
	 
		public DeclarationContext() { }
		public void copyFrom(DeclarationContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class DeclListContext extends DeclarationContext {
		public LinkedListContext linkedList() {
			return getRuleContext(LinkedListContext.class,0);
		}
		public DeclListContext(DeclarationContext ctx) { copyFrom(ctx); }
	}
	public static class DeclClassContext extends DeclarationContext {
		public ClassDecContext classDec() {
			return getRuleContext(ClassDecContext.class,0);
		}
		public DeclClassContext(DeclarationContext ctx) { copyFrom(ctx); }
	}
	public static class DeclPrimitiveContext extends DeclarationContext {
		public PrimitiveContext primitive() {
			return getRuleContext(PrimitiveContext.class,0);
		}
		public DeclPrimitiveContext(DeclarationContext ctx) { copyFrom(ctx); }
	}

	public final DeclarationContext declaration() throws RecognitionException {
		DeclarationContext _localctx = new DeclarationContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_declaration);
		try {
			setState(70);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__10:
			case T__11:
			case T__12:
			case T__13:
			case T__14:
			case T__15:
			case T__16:
			case T__17:
			case IDENTIFIER:
				_localctx = new DeclPrimitiveContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(67);
				primitive();
				}
				break;
			case T__5:
				_localctx = new DeclClassContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(68);
				classDec();
				}
				break;
			case T__0:
				_localctx = new DeclListContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(69);
				linkedList();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LinkedListContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public ArrayInitContext arrayInit() {
			return getRuleContext(ArrayInitContext.class,0);
		}
		public LinkedListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_linkedList; }
	}

	public final LinkedListContext linkedList() throws RecognitionException {
		LinkedListContext _localctx = new LinkedListContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_linkedList);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(72);
			match(T__0);
			setState(73);
			match(T__1);
			setState(74);
			type();
			setState(75);
			match(T__2);
			setState(76);
			identifier();
			setState(77);
			match(T__3);
			setState(78);
			arrayInit();
			setState(79);
			match(T__4);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PrimitiveContext extends ParserRuleContext {
		public PrimitiveContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_primitive; }
	 
		public PrimitiveContext() { }
		public void copyFrom(PrimitiveContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class DeclPrimitiveVarContext extends PrimitiveContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public DeclaratorsContext declarators() {
			return getRuleContext(DeclaratorsContext.class,0);
		}
		public ArrBracketContext arrBracket() {
			return getRuleContext(ArrBracketContext.class,0);
		}
		public DeclPrimitiveVarContext(PrimitiveContext ctx) { copyFrom(ctx); }
	}
	public static class DeclReferenceContext extends PrimitiveContext {
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public DeclaratorsContext declarators() {
			return getRuleContext(DeclaratorsContext.class,0);
		}
		public ArrBracketContext arrBracket() {
			return getRuleContext(ArrBracketContext.class,0);
		}
		public DeclReferenceContext(PrimitiveContext ctx) { copyFrom(ctx); }
	}

	public final PrimitiveContext primitive() throws RecognitionException {
		PrimitiveContext _localctx = new PrimitiveContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_primitive);
		int _la;
		try {
			setState(95);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__10:
			case T__11:
			case T__12:
			case T__13:
			case T__14:
			case T__15:
			case T__16:
			case T__17:
				_localctx = new DeclPrimitiveVarContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(81);
				type();
				setState(83);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__19) {
					{
					setState(82);
					arrBracket();
					}
				}

				setState(85);
				declarators();
				setState(86);
				match(T__4);
				}
				break;
			case IDENTIFIER:
				_localctx = new DeclReferenceContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(88);
				identifier();
				setState(90);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__19) {
					{
					setState(89);
					arrBracket();
					}
				}

				setState(92);
				declarators();
				setState(93);
				match(T__4);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class MemberDeclContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public List<IdentifierContext> identifier() {
			return getRuleContexts(IdentifierContext.class);
		}
		public IdentifierContext identifier(int i) {
			return getRuleContext(IdentifierContext.class,i);
		}
		public MemberDeclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_memberDecl; }
	}

	public final MemberDeclContext memberDecl() throws RecognitionException {
		MemberDeclContext _localctx = new MemberDeclContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_memberDecl);
		try {
			setState(105);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__10:
			case T__11:
			case T__12:
			case T__13:
			case T__14:
			case T__15:
			case T__16:
			case T__17:
				enterOuterAlt(_localctx, 1);
				{
				setState(97);
				type();
				setState(98);
				identifier();
				setState(99);
				match(T__4);
				}
				break;
			case IDENTIFIER:
				enterOuterAlt(_localctx, 2);
				{
				setState(101);
				identifier();
				setState(102);
				identifier();
				setState(103);
				match(T__4);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ClassDecContext extends ParserRuleContext {
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public List<MemberDeclContext> memberDecl() {
			return getRuleContexts(MemberDeclContext.class);
		}
		public MemberDeclContext memberDecl(int i) {
			return getRuleContext(MemberDeclContext.class,i);
		}
		public ClassDecContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_classDec; }
	}

	public final ClassDecContext classDec() throws RecognitionException {
		ClassDecContext _localctx = new ClassDecContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_classDec);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(107);
			match(T__5);
			setState(108);
			identifier();
			setState(109);
			match(T__6);
			setState(111); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(110);
				memberDecl();
				}
				}
				setState(113); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( ((((_la - 11)) & ~0x3f) == 0 && ((1L << (_la - 11)) & ((1L << (T__10 - 11)) | (1L << (T__11 - 11)) | (1L << (T__12 - 11)) | (1L << (T__13 - 11)) | (1L << (T__14 - 11)) | (1L << (T__15 - 11)) | (1L << (T__16 - 11)) | (1L << (T__17 - 11)) | (1L << (IDENTIFIER - 11)))) != 0) );
			setState(115);
			match(T__7);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeContext extends ParserRuleContext {
		public PrimitiveTypesContext primitiveTypes() {
			return getRuleContext(PrimitiveTypesContext.class,0);
		}
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_type);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(117);
			primitiveTypes();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FuncCallContext extends ParserRuleContext {
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public ExpressionListContext expressionList() {
			return getRuleContext(ExpressionListContext.class,0);
		}
		public FuncCallContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_funcCall; }
	}

	public final FuncCallContext funcCall() throws RecognitionException {
		FuncCallContext _localctx = new FuncCallContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_funcCall);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(119);
			identifier();
			setState(120);
			match(T__8);
			setState(122);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 9)) & ~0x3f) == 0 && ((1L << (_la - 9)) & ((1L << (T__8 - 9)) | (1L << (T__21 - 9)) | (1L << (T__23 - 9)) | (1L << (T__24 - 9)) | (1L << (T__25 - 9)) | (1L << (T__26 - 9)) | (1L << (T__27 - 9)) | (1L << (T__28 - 9)) | (1L << (T__29 - 9)) | (1L << (T__30 - 9)) | (1L << (DECIMAL_LITERAL - 9)) | (1L << (HEX_LITERAL - 9)) | (1L << (OCT_LITERAL - 9)) | (1L << (BINARY_LITERAL - 9)) | (1L << (FLOAT_LITERAL - 9)) | (1L << (HEX_FLOAT_LITERAL - 9)) | (1L << (IDENTIFIER - 9)))) != 0)) {
				{
				setState(121);
				expressionList();
				}
			}

			setState(124);
			match(T__9);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PrimitiveTypesContext extends ParserRuleContext {
		public ArithTypeContext arithType() {
			return getRuleContext(ArithTypeContext.class,0);
		}
		public SignedorunsignedContext signedorunsigned() {
			return getRuleContext(SignedorunsignedContext.class,0);
		}
		public PrimitiveTypesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_primitiveTypes; }
	}

	public final PrimitiveTypesContext primitiveTypes() throws RecognitionException {
		PrimitiveTypesContext _localctx = new PrimitiveTypesContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_primitiveTypes);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(127);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__16 || _la==T__17) {
				{
				setState(126);
				signedorunsigned();
				}
			}

			setState(129);
			arithType();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArithTypeContext extends ParserRuleContext {
		public ArithTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arithType; }
	}

	public final ArithTypeContext arithType() throws RecognitionException {
		ArithTypeContext _localctx = new ArithTypeContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_arithType);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(131);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__10) | (1L << T__11) | (1L << T__12) | (1L << T__13) | (1L << T__14) | (1L << T__15))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArithTypeNOINTContext extends ParserRuleContext {
		public ArithTypeNOINTContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arithTypeNOINT; }
	}

	public final ArithTypeNOINTContext arithTypeNOINT() throws RecognitionException {
		ArithTypeNOINTContext _localctx = new ArithTypeNOINTContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_arithTypeNOINT);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(133);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__10) | (1L << T__11) | (1L << T__13))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class SignedorunsignedContext extends ParserRuleContext {
		public SignedorunsignedContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_signedorunsigned; }
	}

	public final SignedorunsignedContext signedorunsigned() throws RecognitionException {
		SignedorunsignedContext _localctx = new SignedorunsignedContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_signedorunsigned);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(135);
			_la = _input.LA(1);
			if ( !(_la==T__16 || _la==T__17) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclaratorsContext extends ParserRuleContext {
		public List<DeclaratorContext> declarator() {
			return getRuleContexts(DeclaratorContext.class);
		}
		public DeclaratorContext declarator(int i) {
			return getRuleContext(DeclaratorContext.class,i);
		}
		public DeclaratorsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declarators; }
	}

	public final DeclaratorsContext declarators() throws RecognitionException {
		DeclaratorsContext _localctx = new DeclaratorsContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_declarators);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(137);
			declarator();
			setState(142);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__18) {
				{
				{
				setState(138);
				match(T__18);
				setState(139);
				declarator();
				}
				}
				setState(144);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclaratorContext extends ParserRuleContext {
		public DeclIdContext declId() {
			return getRuleContext(DeclIdContext.class,0);
		}
		public InitializerContext initializer() {
			return getRuleContext(InitializerContext.class,0);
		}
		public DeclaratorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declarator; }
	}

	public final DeclaratorContext declarator() throws RecognitionException {
		DeclaratorContext _localctx = new DeclaratorContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_declarator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(145);
			declId();
			setState(148);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__3) {
				{
				setState(146);
				match(T__3);
				setState(147);
				initializer();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclIdContext extends ParserRuleContext {
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public ArrBracketContext arrBracket() {
			return getRuleContext(ArrBracketContext.class,0);
		}
		public DeclIdContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declId; }
	}

	public final DeclIdContext declId() throws RecognitionException {
		DeclIdContext _localctx = new DeclIdContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_declId);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(150);
			identifier();
			setState(152);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__19) {
				{
				setState(151);
				arrBracket();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArrBracketContext extends ParserRuleContext {
		public List<IntegerLiteralContext> integerLiteral() {
			return getRuleContexts(IntegerLiteralContext.class);
		}
		public IntegerLiteralContext integerLiteral(int i) {
			return getRuleContext(IntegerLiteralContext.class,i);
		}
		public ArrBracketContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arrBracket; }
	}

	public final ArrBracketContext arrBracket() throws RecognitionException {
		ArrBracketContext _localctx = new ArrBracketContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_arrBracket);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(154);
			match(T__19);
			setState(158);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << DECIMAL_LITERAL) | (1L << HEX_LITERAL) | (1L << OCT_LITERAL) | (1L << BINARY_LITERAL))) != 0)) {
				{
				{
				setState(155);
				integerLiteral();
				}
				}
				setState(160);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(161);
			match(T__20);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PointerContext extends ParserRuleContext {
		public PointerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pointer; }
	}

	public final PointerContext pointer() throws RecognitionException {
		PointerContext _localctx = new PointerContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_pointer);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(163);
			match(T__21);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionContext extends ParserRuleContext {
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	 
		public ExpressionContext() { }
		public void copyFrom(ExpressionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class ExprConstructorContext extends ExpressionContext {
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public ExprConstructorContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprTernaryContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprTernaryContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprComparisonContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprComparisonContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprIdentifierContext extends ExpressionContext {
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public ExprIdentifierContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprBitwiseORContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprBitwiseORContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprASContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprASContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprPrefixArthemticContext extends ExpressionContext {
		public Token prefix;
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ExprPrefixArthemticContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprIndirectionContext extends ExpressionContext {
		public Token prefix;
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ExprIndirectionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprLogicalORContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprLogicalORContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprRightAssociationContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprRightAssociationContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprShiftingContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprShiftingContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprPostfixContext extends ExpressionContext {
		public Token postfix;
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ExprPostfixContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprPrimaryContext extends ExpressionContext {
		public PrimaryContext primary() {
			return getRuleContext(PrimaryContext.class,0);
		}
		public ExprPrimaryContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprMDMContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprMDMContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprFuncCallContext extends ExpressionContext {
		public FuncCallContext funcCall() {
			return getRuleContext(FuncCallContext.class,0);
		}
		public ExprFuncCallContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprXORContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprXORContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprMemberAcessContext extends ExpressionContext {
		public Token bop;
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public ExprMemberAcessContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprGroupedExpressionContext extends ExpressionContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ExprGroupedExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprPrefixLogicalContext extends ExpressionContext {
		public Token prefix;
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ExprPrefixLogicalContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprBitwiseANDContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprBitwiseANDContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprEqualityContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprEqualityContext(ExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ExprLogicalANDContext extends ExpressionContext {
		public Token bop;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExprLogicalANDContext(ExpressionContext ctx) { copyFrom(ctx); }
	}

	public final ExpressionContext expression() throws RecognitionException {
		return expression(0);
	}

	private ExpressionContext expression(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExpressionContext _localctx = new ExpressionContext(_ctx, _parentState);
		ExpressionContext _prevctx = _localctx;
		int _startState = 36;
		enterRecursionRule(_localctx, 36, RULE_expression, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(184);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,14,_ctx) ) {
			case 1:
				{
				_localctx = new ExprPrimaryContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(166);
				primary();
				}
				break;
			case 2:
				{
				_localctx = new ExprIdentifierContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(167);
				identifier();
				}
				break;
			case 3:
				{
				_localctx = new ExprGroupedExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(168);
				match(T__8);
				setState(169);
				expression(0);
				setState(170);
				match(T__9);
				}
				break;
			case 4:
				{
				_localctx = new ExprFuncCallContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(172);
				funcCall();
				}
				break;
			case 5:
				{
				_localctx = new ExprConstructorContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(173);
				match(T__23);
				setState(174);
				identifier();
				setState(175);
				match(T__8);
				setState(176);
				match(T__9);
				}
				break;
			case 6:
				{
				_localctx = new ExprPrefixArthemticContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(178);
				((ExprPrefixArthemticContext)_localctx).prefix = _input.LT(1);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__24) | (1L << T__25) | (1L << T__26) | (1L << T__27))) != 0)) ) {
					((ExprPrefixArthemticContext)_localctx).prefix = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(179);
				expression(15);
				}
				break;
			case 7:
				{
				_localctx = new ExprPrefixLogicalContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(180);
				((ExprPrefixLogicalContext)_localctx).prefix = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==T__28 || _la==T__29) ) {
					((ExprPrefixLogicalContext)_localctx).prefix = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(181);
				expression(14);
				}
				break;
			case 8:
				{
				_localctx = new ExprIndirectionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(182);
				((ExprIndirectionContext)_localctx).prefix = _input.LT(1);
				_la = _input.LA(1);
				if ( !(_la==T__21 || _la==T__30) ) {
					((ExprIndirectionContext)_localctx).prefix = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(183);
				expression(13);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(240);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,17,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(238);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,16,_ctx) ) {
					case 1:
						{
						_localctx = new ExprMDMContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(186);
						if (!(precpred(_ctx, 12))) throw new FailedPredicateException(this, "precpred(_ctx, 12)");
						setState(187);
						((ExprMDMContext)_localctx).bop = _input.LT(1);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__21) | (1L << T__31) | (1L << T__32))) != 0)) ) {
							((ExprMDMContext)_localctx).bop = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(188);
						expression(13);
						}
						break;
					case 2:
						{
						_localctx = new ExprASContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(189);
						if (!(precpred(_ctx, 11))) throw new FailedPredicateException(this, "precpred(_ctx, 11)");
						setState(190);
						((ExprASContext)_localctx).bop = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__26 || _la==T__27) ) {
							((ExprASContext)_localctx).bop = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(191);
						expression(12);
						}
						break;
					case 3:
						{
						_localctx = new ExprShiftingContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(192);
						if (!(precpred(_ctx, 10))) throw new FailedPredicateException(this, "precpred(_ctx, 10)");
						setState(200);
						_errHandler.sync(this);
						switch ( getInterpreter().adaptivePredict(_input,15,_ctx) ) {
						case 1:
							{
							setState(193);
							match(T__1);
							setState(194);
							match(T__1);
							}
							break;
						case 2:
							{
							setState(195);
							match(T__2);
							setState(196);
							match(T__2);
							setState(197);
							match(T__2);
							}
							break;
						case 3:
							{
							setState(198);
							match(T__2);
							setState(199);
							match(T__2);
							}
							break;
						}
						setState(202);
						expression(11);
						}
						break;
					case 4:
						{
						_localctx = new ExprComparisonContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(203);
						if (!(precpred(_ctx, 9))) throw new FailedPredicateException(this, "precpred(_ctx, 9)");
						setState(204);
						((ExprComparisonContext)_localctx).bop = _input.LT(1);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__2) | (1L << T__33) | (1L << T__34))) != 0)) ) {
							((ExprComparisonContext)_localctx).bop = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(205);
						expression(10);
						}
						break;
					case 5:
						{
						_localctx = new ExprEqualityContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(206);
						if (!(precpred(_ctx, 8))) throw new FailedPredicateException(this, "precpred(_ctx, 8)");
						setState(207);
						((ExprEqualityContext)_localctx).bop = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__35 || _la==T__36) ) {
							((ExprEqualityContext)_localctx).bop = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(208);
						expression(9);
						}
						break;
					case 6:
						{
						_localctx = new ExprBitwiseANDContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(209);
						if (!(precpred(_ctx, 7))) throw new FailedPredicateException(this, "precpred(_ctx, 7)");
						setState(210);
						((ExprBitwiseANDContext)_localctx).bop = match(T__30);
						setState(211);
						expression(8);
						}
						break;
					case 7:
						{
						_localctx = new ExprXORContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(212);
						if (!(precpred(_ctx, 6))) throw new FailedPredicateException(this, "precpred(_ctx, 6)");
						setState(213);
						((ExprXORContext)_localctx).bop = match(T__37);
						setState(214);
						expression(7);
						}
						break;
					case 8:
						{
						_localctx = new ExprBitwiseORContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(215);
						if (!(precpred(_ctx, 5))) throw new FailedPredicateException(this, "precpred(_ctx, 5)");
						setState(216);
						((ExprBitwiseORContext)_localctx).bop = match(T__38);
						setState(217);
						expression(6);
						}
						break;
					case 9:
						{
						_localctx = new ExprLogicalANDContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(218);
						if (!(precpred(_ctx, 4))) throw new FailedPredicateException(this, "precpred(_ctx, 4)");
						setState(219);
						((ExprLogicalANDContext)_localctx).bop = match(T__39);
						setState(220);
						expression(5);
						}
						break;
					case 10:
						{
						_localctx = new ExprLogicalORContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(221);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(222);
						((ExprLogicalORContext)_localctx).bop = match(T__40);
						setState(223);
						expression(4);
						}
						break;
					case 11:
						{
						_localctx = new ExprTernaryContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(224);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(225);
						((ExprTernaryContext)_localctx).bop = match(T__41);
						setState(226);
						expression(0);
						setState(227);
						match(T__42);
						setState(228);
						expression(2);
						}
						break;
					case 12:
						{
						_localctx = new ExprRightAssociationContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(230);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(231);
						((ExprRightAssociationContext)_localctx).bop = _input.LT(1);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__3) | (1L << T__43) | (1L << T__44) | (1L << T__45) | (1L << T__46) | (1L << T__47) | (1L << T__48) | (1L << T__49) | (1L << T__50) | (1L << T__51) | (1L << T__52))) != 0)) ) {
							((ExprRightAssociationContext)_localctx).bop = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(232);
						expression(1);
						}
						break;
					case 13:
						{
						_localctx = new ExprMemberAcessContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(233);
						if (!(precpred(_ctx, 19))) throw new FailedPredicateException(this, "precpred(_ctx, 19)");
						setState(234);
						((ExprMemberAcessContext)_localctx).bop = match(T__22);
						setState(235);
						identifier();
						}
						break;
					case 14:
						{
						_localctx = new ExprPostfixContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(236);
						if (!(precpred(_ctx, 16))) throw new FailedPredicateException(this, "precpred(_ctx, 16)");
						setState(237);
						((ExprPostfixContext)_localctx).postfix = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__24 || _la==T__25) ) {
							((ExprPostfixContext)_localctx).postfix = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						}
						break;
					}
					} 
				}
				setState(242);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,17,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class InitializerContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ArrayInitContext arrayInit() {
			return getRuleContext(ArrayInitContext.class,0);
		}
		public InitializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_initializer; }
	}

	public final InitializerContext initializer() throws RecognitionException {
		InitializerContext _localctx = new InitializerContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_initializer);
		try {
			setState(245);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__8:
			case T__21:
			case T__23:
			case T__24:
			case T__25:
			case T__26:
			case T__27:
			case T__28:
			case T__29:
			case T__30:
			case DECIMAL_LITERAL:
			case HEX_LITERAL:
			case OCT_LITERAL:
			case BINARY_LITERAL:
			case FLOAT_LITERAL:
			case HEX_FLOAT_LITERAL:
			case IDENTIFIER:
				enterOuterAlt(_localctx, 1);
				{
				setState(243);
				expression(0);
				}
				break;
			case T__6:
				enterOuterAlt(_localctx, 2);
				{
				setState(244);
				arrayInit();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArrayInitContext extends ParserRuleContext {
		public List<InitializerContext> initializer() {
			return getRuleContexts(InitializerContext.class);
		}
		public InitializerContext initializer(int i) {
			return getRuleContext(InitializerContext.class,i);
		}
		public ArrayInitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arrayInit; }
	}

	public final ArrayInitContext arrayInit() throws RecognitionException {
		ArrayInitContext _localctx = new ArrayInitContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_arrayInit);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(247);
			match(T__6);
			setState(259);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 7)) & ~0x3f) == 0 && ((1L << (_la - 7)) & ((1L << (T__6 - 7)) | (1L << (T__8 - 7)) | (1L << (T__21 - 7)) | (1L << (T__23 - 7)) | (1L << (T__24 - 7)) | (1L << (T__25 - 7)) | (1L << (T__26 - 7)) | (1L << (T__27 - 7)) | (1L << (T__28 - 7)) | (1L << (T__29 - 7)) | (1L << (T__30 - 7)) | (1L << (DECIMAL_LITERAL - 7)) | (1L << (HEX_LITERAL - 7)) | (1L << (OCT_LITERAL - 7)) | (1L << (BINARY_LITERAL - 7)) | (1L << (FLOAT_LITERAL - 7)) | (1L << (HEX_FLOAT_LITERAL - 7)) | (1L << (IDENTIFIER - 7)))) != 0)) {
				{
				setState(248);
				initializer();
				setState(253);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,19,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(249);
						match(T__18);
						setState(250);
						initializer();
						}
						} 
					}
					setState(255);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,19,_ctx);
				}
				setState(257);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__18) {
					{
					setState(256);
					match(T__18);
					}
				}

				}
			}

			setState(261);
			match(T__7);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StatementContext extends ParserRuleContext {
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	 
		public StatementContext() { }
		public void copyFrom(StatementContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class StmtReturnContext extends StatementContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public StmtReturnContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class StmtWhileContext extends StatementContext {
		public ExprparContext exprpar() {
			return getRuleContext(ExprparContext.class,0);
		}
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public StmtWhileContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class EmptyStatementContext extends StatementContext {
		public EmptyStatementContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class StmtExpressionContext extends StatementContext {
		public ExpressionContext statementExpression;
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public StmtExpressionContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class StmtDoWhileContext extends StatementContext {
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public ExprparContext exprpar() {
			return getRuleContext(ExprparContext.class,0);
		}
		public StmtDoWhileContext(StatementContext ctx) { copyFrom(ctx); }
	}
	public static class StmtIfContext extends StatementContext {
		public ExprparContext exprpar() {
			return getRuleContext(ExprparContext.class,0);
		}
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public StmtIfContext(StatementContext ctx) { copyFrom(ctx); }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_statement);
		int _la;
		try {
			setState(289);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__53:
				_localctx = new StmtIfContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(263);
				match(T__53);
				setState(264);
				exprpar();
				setState(265);
				statement();
				setState(268);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,22,_ctx) ) {
				case 1:
					{
					setState(266);
					match(T__54);
					setState(267);
					statement();
					}
					break;
				}
				}
				break;
			case T__55:
				_localctx = new StmtWhileContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(270);
				match(T__55);
				setState(271);
				exprpar();
				setState(272);
				statement();
				}
				break;
			case T__56:
				_localctx = new StmtDoWhileContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(274);
				match(T__56);
				setState(275);
				statement();
				setState(276);
				match(T__55);
				setState(277);
				exprpar();
				setState(278);
				match(T__4);
				}
				break;
			case T__57:
				_localctx = new StmtReturnContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(280);
				match(T__57);
				setState(282);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (((((_la - 9)) & ~0x3f) == 0 && ((1L << (_la - 9)) & ((1L << (T__8 - 9)) | (1L << (T__21 - 9)) | (1L << (T__23 - 9)) | (1L << (T__24 - 9)) | (1L << (T__25 - 9)) | (1L << (T__26 - 9)) | (1L << (T__27 - 9)) | (1L << (T__28 - 9)) | (1L << (T__29 - 9)) | (1L << (T__30 - 9)) | (1L << (DECIMAL_LITERAL - 9)) | (1L << (HEX_LITERAL - 9)) | (1L << (OCT_LITERAL - 9)) | (1L << (BINARY_LITERAL - 9)) | (1L << (FLOAT_LITERAL - 9)) | (1L << (HEX_FLOAT_LITERAL - 9)) | (1L << (IDENTIFIER - 9)))) != 0)) {
					{
					setState(281);
					expression(0);
					}
				}

				setState(284);
				match(T__4);
				}
				break;
			case T__8:
			case T__21:
			case T__23:
			case T__24:
			case T__25:
			case T__26:
			case T__27:
			case T__28:
			case T__29:
			case T__30:
			case DECIMAL_LITERAL:
			case HEX_LITERAL:
			case OCT_LITERAL:
			case BINARY_LITERAL:
			case FLOAT_LITERAL:
			case HEX_FLOAT_LITERAL:
			case IDENTIFIER:
				_localctx = new StmtExpressionContext(_localctx);
				enterOuterAlt(_localctx, 5);
				{
				setState(285);
				((StmtExpressionContext)_localctx).statementExpression = expression(0);
				setState(286);
				match(T__4);
				}
				break;
			case T__4:
				_localctx = new EmptyStatementContext(_localctx);
				enterOuterAlt(_localctx, 6);
				{
				setState(288);
				match(T__4);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExprparContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ExprparContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_exprpar; }
	}

	public final ExprparContext exprpar() throws RecognitionException {
		ExprparContext _localctx = new ExprparContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_exprpar);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(291);
			match(T__8);
			setState(292);
			expression(0);
			setState(293);
			match(T__9);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PrimaryContext extends ParserRuleContext {
		public PrimaryContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_primary; }
	 
		public PrimaryContext() { }
		public void copyFrom(PrimaryContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class PrimaryIdentifierContext extends PrimaryContext {
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public PrimaryIdentifierContext(PrimaryContext ctx) { copyFrom(ctx); }
	}
	public static class PrimaryLiteralContext extends PrimaryContext {
		public LiteralContext literal() {
			return getRuleContext(LiteralContext.class,0);
		}
		public PrimaryLiteralContext(PrimaryContext ctx) { copyFrom(ctx); }
	}

	public final PrimaryContext primary() throws RecognitionException {
		PrimaryContext _localctx = new PrimaryContext(_ctx, getState());
		enterRule(_localctx, 46, RULE_primary);
		try {
			setState(297);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case DECIMAL_LITERAL:
			case HEX_LITERAL:
			case OCT_LITERAL:
			case BINARY_LITERAL:
			case FLOAT_LITERAL:
			case HEX_FLOAT_LITERAL:
				_localctx = new PrimaryLiteralContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(295);
				literal();
				}
				break;
			case IDENTIFIER:
				_localctx = new PrimaryIdentifierContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(296);
				identifier();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IdentifierContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(JavaParser.IDENTIFIER, 0); }
		public IdentifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_identifier; }
	}

	public final IdentifierContext identifier() throws RecognitionException {
		IdentifierContext _localctx = new IdentifierContext(_ctx, getState());
		enterRule(_localctx, 48, RULE_identifier);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(299);
			match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LiteralContext extends ParserRuleContext {
		public IntegerLiteralContext integerLiteral() {
			return getRuleContext(IntegerLiteralContext.class,0);
		}
		public FloatLiteralContext floatLiteral() {
			return getRuleContext(FloatLiteralContext.class,0);
		}
		public LiteralContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_literal; }
	}

	public final LiteralContext literal() throws RecognitionException {
		LiteralContext _localctx = new LiteralContext(_ctx, getState());
		enterRule(_localctx, 50, RULE_literal);
		try {
			setState(303);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case DECIMAL_LITERAL:
			case HEX_LITERAL:
			case OCT_LITERAL:
			case BINARY_LITERAL:
				enterOuterAlt(_localctx, 1);
				{
				setState(301);
				integerLiteral();
				}
				break;
			case FLOAT_LITERAL:
			case HEX_FLOAT_LITERAL:
				enterOuterAlt(_localctx, 2);
				{
				setState(302);
				floatLiteral();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IntegerLiteralContext extends ParserRuleContext {
		public TerminalNode DECIMAL_LITERAL() { return getToken(JavaParser.DECIMAL_LITERAL, 0); }
		public TerminalNode HEX_LITERAL() { return getToken(JavaParser.HEX_LITERAL, 0); }
		public TerminalNode OCT_LITERAL() { return getToken(JavaParser.OCT_LITERAL, 0); }
		public TerminalNode BINARY_LITERAL() { return getToken(JavaParser.BINARY_LITERAL, 0); }
		public IntegerLiteralContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_integerLiteral; }
	}

	public final IntegerLiteralContext integerLiteral() throws RecognitionException {
		IntegerLiteralContext _localctx = new IntegerLiteralContext(_ctx, getState());
		enterRule(_localctx, 52, RULE_integerLiteral);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(305);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << DECIMAL_LITERAL) | (1L << HEX_LITERAL) | (1L << OCT_LITERAL) | (1L << BINARY_LITERAL))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FloatLiteralContext extends ParserRuleContext {
		public TerminalNode FLOAT_LITERAL() { return getToken(JavaParser.FLOAT_LITERAL, 0); }
		public TerminalNode HEX_FLOAT_LITERAL() { return getToken(JavaParser.HEX_FLOAT_LITERAL, 0); }
		public FloatLiteralContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_floatLiteral; }
	}

	public final FloatLiteralContext floatLiteral() throws RecognitionException {
		FloatLiteralContext _localctx = new FloatLiteralContext(_ctx, getState());
		enterRule(_localctx, 54, RULE_floatLiteral);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(307);
			_la = _input.LA(1);
			if ( !(_la==FLOAT_LITERAL || _la==HEX_FLOAT_LITERAL) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionListContext extends ParserRuleContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public ExpressionListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expressionList; }
	}

	public final ExpressionListContext expressionList() throws RecognitionException {
		ExpressionListContext _localctx = new ExpressionListContext(_ctx, getState());
		enterRule(_localctx, 56, RULE_expressionList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(309);
			expression(0);
			setState(314);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__18) {
				{
				{
				setState(310);
				match(T__18);
				setState(311);
				expression(0);
				}
				}
				setState(316);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 18:
			return expression_sempred((ExpressionContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expression_sempred(ExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 12);
		case 1:
			return precpred(_ctx, 11);
		case 2:
			return precpred(_ctx, 10);
		case 3:
			return precpred(_ctx, 9);
		case 4:
			return precpred(_ctx, 8);
		case 5:
			return precpred(_ctx, 7);
		case 6:
			return precpred(_ctx, 6);
		case 7:
			return precpred(_ctx, 5);
		case 8:
			return precpred(_ctx, 4);
		case 9:
			return precpred(_ctx, 3);
		case 10:
			return precpred(_ctx, 2);
		case 11:
			return precpred(_ctx, 1);
		case 12:
			return precpred(_ctx, 19);
		case 13:
			return precpred(_ctx, 16);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3F\u0140\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\3\2\6\2>\n\2\r\2\16"+
		"\2?\3\3\3\3\5\3D\n\3\3\4\3\4\3\4\5\4I\n\4\3\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\6\3\6\5\6V\n\6\3\6\3\6\3\6\3\6\3\6\5\6]\n\6\3\6\3\6\3\6\5\6"+
		"b\n\6\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\5\7l\n\7\3\b\3\b\3\b\3\b\6\br\n"+
		"\b\r\b\16\bs\3\b\3\b\3\t\3\t\3\n\3\n\3\n\5\n}\n\n\3\n\3\n\3\13\5\13\u0082"+
		"\n\13\3\13\3\13\3\f\3\f\3\r\3\r\3\16\3\16\3\17\3\17\3\17\7\17\u008f\n"+
		"\17\f\17\16\17\u0092\13\17\3\20\3\20\3\20\5\20\u0097\n\20\3\21\3\21\5"+
		"\21\u009b\n\21\3\22\3\22\7\22\u009f\n\22\f\22\16\22\u00a2\13\22\3\22\3"+
		"\22\3\23\3\23\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3"+
		"\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\5\24\u00bb\n\24\3\24\3\24\3\24"+
		"\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\5\24\u00cb\n\24"+
		"\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24"+
		"\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24"+
		"\3\24\3\24\3\24\3\24\3\24\3\24\3\24\3\24\7\24\u00f1\n\24\f\24\16\24\u00f4"+
		"\13\24\3\25\3\25\5\25\u00f8\n\25\3\26\3\26\3\26\3\26\7\26\u00fe\n\26\f"+
		"\26\16\26\u0101\13\26\3\26\5\26\u0104\n\26\5\26\u0106\n\26\3\26\3\26\3"+
		"\27\3\27\3\27\3\27\3\27\5\27\u010f\n\27\3\27\3\27\3\27\3\27\3\27\3\27"+
		"\3\27\3\27\3\27\3\27\3\27\3\27\5\27\u011d\n\27\3\27\3\27\3\27\3\27\3\27"+
		"\5\27\u0124\n\27\3\30\3\30\3\30\3\30\3\31\3\31\5\31\u012c\n\31\3\32\3"+
		"\32\3\33\3\33\5\33\u0132\n\33\3\34\3\34\3\35\3\35\3\36\3\36\3\36\7\36"+
		"\u013b\n\36\f\36\16\36\u013e\13\36\3\36\2\3&\37\2\4\6\b\n\f\16\20\22\24"+
		"\26\30\32\34\36 \"$&(*,.\60\62\64\668:\2\20\3\2\r\22\4\2\r\16\20\20\3"+
		"\2\23\24\3\2\33\36\3\2\37 \4\2\30\30!!\4\2\30\30\"#\3\2\35\36\4\2\4\5"+
		"$%\3\2&\'\4\2\6\6.\67\3\2\33\34\3\2=@\3\2AB\2\u0156\2=\3\2\2\2\4C\3\2"+
		"\2\2\6H\3\2\2\2\bJ\3\2\2\2\na\3\2\2\2\fk\3\2\2\2\16m\3\2\2\2\20w\3\2\2"+
		"\2\22y\3\2\2\2\24\u0081\3\2\2\2\26\u0085\3\2\2\2\30\u0087\3\2\2\2\32\u0089"+
		"\3\2\2\2\34\u008b\3\2\2\2\36\u0093\3\2\2\2 \u0098\3\2\2\2\"\u009c\3\2"+
		"\2\2$\u00a5\3\2\2\2&\u00ba\3\2\2\2(\u00f7\3\2\2\2*\u00f9\3\2\2\2,\u0123"+
		"\3\2\2\2.\u0125\3\2\2\2\60\u012b\3\2\2\2\62\u012d\3\2\2\2\64\u0131\3\2"+
		"\2\2\66\u0133\3\2\2\28\u0135\3\2\2\2:\u0137\3\2\2\2<>\5\4\3\2=<\3\2\2"+
		"\2>?\3\2\2\2?=\3\2\2\2?@\3\2\2\2@\3\3\2\2\2AD\5,\27\2BD\5\6\4\2CA\3\2"+
		"\2\2CB\3\2\2\2D\5\3\2\2\2EI\5\n\6\2FI\5\16\b\2GI\5\b\5\2HE\3\2\2\2HF\3"+
		"\2\2\2HG\3\2\2\2I\7\3\2\2\2JK\7\3\2\2KL\7\4\2\2LM\5\20\t\2MN\7\5\2\2N"+
		"O\5\62\32\2OP\7\6\2\2PQ\5*\26\2QR\7\7\2\2R\t\3\2\2\2SU\5\20\t\2TV\5\""+
		"\22\2UT\3\2\2\2UV\3\2\2\2VW\3\2\2\2WX\5\34\17\2XY\7\7\2\2Yb\3\2\2\2Z\\"+
		"\5\62\32\2[]\5\"\22\2\\[\3\2\2\2\\]\3\2\2\2]^\3\2\2\2^_\5\34\17\2_`\7"+
		"\7\2\2`b\3\2\2\2aS\3\2\2\2aZ\3\2\2\2b\13\3\2\2\2cd\5\20\t\2de\5\62\32"+
		"\2ef\7\7\2\2fl\3\2\2\2gh\5\62\32\2hi\5\62\32\2ij\7\7\2\2jl\3\2\2\2kc\3"+
		"\2\2\2kg\3\2\2\2l\r\3\2\2\2mn\7\b\2\2no\5\62\32\2oq\7\t\2\2pr\5\f\7\2"+
		"qp\3\2\2\2rs\3\2\2\2sq\3\2\2\2st\3\2\2\2tu\3\2\2\2uv\7\n\2\2v\17\3\2\2"+
		"\2wx\5\24\13\2x\21\3\2\2\2yz\5\62\32\2z|\7\13\2\2{}\5:\36\2|{\3\2\2\2"+
		"|}\3\2\2\2}~\3\2\2\2~\177\7\f\2\2\177\23\3\2\2\2\u0080\u0082\5\32\16\2"+
		"\u0081\u0080\3\2\2\2\u0081\u0082\3\2\2\2\u0082\u0083\3\2\2\2\u0083\u0084"+
		"\5\26\f\2\u0084\25\3\2\2\2\u0085\u0086\t\2\2\2\u0086\27\3\2\2\2\u0087"+
		"\u0088\t\3\2\2\u0088\31\3\2\2\2\u0089\u008a\t\4\2\2\u008a\33\3\2\2\2\u008b"+
		"\u0090\5\36\20\2\u008c\u008d\7\25\2\2\u008d\u008f\5\36\20\2\u008e\u008c"+
		"\3\2\2\2\u008f\u0092\3\2\2\2\u0090\u008e\3\2\2\2\u0090\u0091\3\2\2\2\u0091"+
		"\35\3\2\2\2\u0092\u0090\3\2\2\2\u0093\u0096\5 \21\2\u0094\u0095\7\6\2"+
		"\2\u0095\u0097\5(\25\2\u0096\u0094\3\2\2\2\u0096\u0097\3\2\2\2\u0097\37"+
		"\3\2\2\2\u0098\u009a\5\62\32\2\u0099\u009b\5\"\22\2\u009a\u0099\3\2\2"+
		"\2\u009a\u009b\3\2\2\2\u009b!\3\2\2\2\u009c\u00a0\7\26\2\2\u009d\u009f"+
		"\5\66\34\2\u009e\u009d\3\2\2\2\u009f\u00a2\3\2\2\2\u00a0\u009e\3\2\2\2"+
		"\u00a0\u00a1\3\2\2\2\u00a1\u00a3\3\2\2\2\u00a2\u00a0\3\2\2\2\u00a3\u00a4"+
		"\7\27\2\2\u00a4#\3\2\2\2\u00a5\u00a6\7\30\2\2\u00a6%\3\2\2\2\u00a7\u00a8"+
		"\b\24\1\2\u00a8\u00bb\5\60\31\2\u00a9\u00bb\5\62\32\2\u00aa\u00ab\7\13"+
		"\2\2\u00ab\u00ac\5&\24\2\u00ac\u00ad\7\f\2\2\u00ad\u00bb\3\2\2\2\u00ae"+
		"\u00bb\5\22\n\2\u00af\u00b0\7\32\2\2\u00b0\u00b1\5\62\32\2\u00b1\u00b2"+
		"\7\13\2\2\u00b2\u00b3\7\f\2\2\u00b3\u00bb\3\2\2\2\u00b4\u00b5\t\5\2\2"+
		"\u00b5\u00bb\5&\24\21\u00b6\u00b7\t\6\2\2\u00b7\u00bb\5&\24\20\u00b8\u00b9"+
		"\t\7\2\2\u00b9\u00bb\5&\24\17\u00ba\u00a7\3\2\2\2\u00ba\u00a9\3\2\2\2"+
		"\u00ba\u00aa\3\2\2\2\u00ba\u00ae\3\2\2\2\u00ba\u00af\3\2\2\2\u00ba\u00b4"+
		"\3\2\2\2\u00ba\u00b6\3\2\2\2\u00ba\u00b8\3\2\2\2\u00bb\u00f2\3\2\2\2\u00bc"+
		"\u00bd\f\16\2\2\u00bd\u00be\t\b\2\2\u00be\u00f1\5&\24\17\u00bf\u00c0\f"+
		"\r\2\2\u00c0\u00c1\t\t\2\2\u00c1\u00f1\5&\24\16\u00c2\u00ca\f\f\2\2\u00c3"+
		"\u00c4\7\4\2\2\u00c4\u00cb\7\4\2\2\u00c5\u00c6\7\5\2\2\u00c6\u00c7\7\5"+
		"\2\2\u00c7\u00cb\7\5\2\2\u00c8\u00c9\7\5\2\2\u00c9\u00cb\7\5\2\2\u00ca"+
		"\u00c3\3\2\2\2\u00ca\u00c5\3\2\2\2\u00ca\u00c8\3\2\2\2\u00cb\u00cc\3\2"+
		"\2\2\u00cc\u00f1\5&\24\r\u00cd\u00ce\f\13\2\2\u00ce\u00cf\t\n\2\2\u00cf"+
		"\u00f1\5&\24\f\u00d0\u00d1\f\n\2\2\u00d1\u00d2\t\13\2\2\u00d2\u00f1\5"+
		"&\24\13\u00d3\u00d4\f\t\2\2\u00d4\u00d5\7!\2\2\u00d5\u00f1\5&\24\n\u00d6"+
		"\u00d7\f\b\2\2\u00d7\u00d8\7(\2\2\u00d8\u00f1\5&\24\t\u00d9\u00da\f\7"+
		"\2\2\u00da\u00db\7)\2\2\u00db\u00f1\5&\24\b\u00dc\u00dd\f\6\2\2\u00dd"+
		"\u00de\7*\2\2\u00de\u00f1\5&\24\7\u00df\u00e0\f\5\2\2\u00e0\u00e1\7+\2"+
		"\2\u00e1\u00f1\5&\24\6\u00e2\u00e3\f\4\2\2\u00e3\u00e4\7,\2\2\u00e4\u00e5"+
		"\5&\24\2\u00e5\u00e6\7-\2\2\u00e6\u00e7\5&\24\4\u00e7\u00f1\3\2\2\2\u00e8"+
		"\u00e9\f\3\2\2\u00e9\u00ea\t\f\2\2\u00ea\u00f1\5&\24\3\u00eb\u00ec\f\25"+
		"\2\2\u00ec\u00ed\7\31\2\2\u00ed\u00f1\5\62\32\2\u00ee\u00ef\f\22\2\2\u00ef"+
		"\u00f1\t\r\2\2\u00f0\u00bc\3\2\2\2\u00f0\u00bf\3\2\2\2\u00f0\u00c2\3\2"+
		"\2\2\u00f0\u00cd\3\2\2\2\u00f0\u00d0\3\2\2\2\u00f0\u00d3\3\2\2\2\u00f0"+
		"\u00d6\3\2\2\2\u00f0\u00d9\3\2\2\2\u00f0\u00dc\3\2\2\2\u00f0\u00df\3\2"+
		"\2\2\u00f0\u00e2\3\2\2\2\u00f0\u00e8\3\2\2\2\u00f0\u00eb\3\2\2\2\u00f0"+
		"\u00ee\3\2\2\2\u00f1\u00f4\3\2\2\2\u00f2\u00f0\3\2\2\2\u00f2\u00f3\3\2"+
		"\2\2\u00f3\'\3\2\2\2\u00f4\u00f2\3\2\2\2\u00f5\u00f8\5&\24\2\u00f6\u00f8"+
		"\5*\26\2\u00f7\u00f5\3\2\2\2\u00f7\u00f6\3\2\2\2\u00f8)\3\2\2\2\u00f9"+
		"\u0105\7\t\2\2\u00fa\u00ff\5(\25\2\u00fb\u00fc\7\25\2\2\u00fc\u00fe\5"+
		"(\25\2\u00fd\u00fb\3\2\2\2\u00fe\u0101\3\2\2\2\u00ff\u00fd\3\2\2\2\u00ff"+
		"\u0100\3\2\2\2\u0100\u0103\3\2\2\2\u0101\u00ff\3\2\2\2\u0102\u0104\7\25"+
		"\2\2\u0103\u0102\3\2\2\2\u0103\u0104\3\2\2\2\u0104\u0106\3\2\2\2\u0105"+
		"\u00fa\3\2\2\2\u0105\u0106\3\2\2\2\u0106\u0107\3\2\2\2\u0107\u0108\7\n"+
		"\2\2\u0108+\3\2\2\2\u0109\u010a\78\2\2\u010a\u010b\5.\30\2\u010b\u010e"+
		"\5,\27\2\u010c\u010d\79\2\2\u010d\u010f\5,\27\2\u010e\u010c\3\2\2\2\u010e"+
		"\u010f\3\2\2\2\u010f\u0124\3\2\2\2\u0110\u0111\7:\2\2\u0111\u0112\5.\30"+
		"\2\u0112\u0113\5,\27\2\u0113\u0124\3\2\2\2\u0114\u0115\7;\2\2\u0115\u0116"+
		"\5,\27\2\u0116\u0117\7:\2\2\u0117\u0118\5.\30\2\u0118\u0119\7\7\2\2\u0119"+
		"\u0124\3\2\2\2\u011a\u011c\7<\2\2\u011b\u011d\5&\24\2\u011c\u011b\3\2"+
		"\2\2\u011c\u011d\3\2\2\2\u011d\u011e\3\2\2\2\u011e\u0124\7\7\2\2\u011f"+
		"\u0120\5&\24\2\u0120\u0121\7\7\2\2\u0121\u0124\3\2\2\2\u0122\u0124\7\7"+
		"\2\2\u0123\u0109\3\2\2\2\u0123\u0110\3\2\2\2\u0123\u0114\3\2\2\2\u0123"+
		"\u011a\3\2\2\2\u0123\u011f\3\2\2\2\u0123\u0122\3\2\2\2\u0124-\3\2\2\2"+
		"\u0125\u0126\7\13\2\2\u0126\u0127\5&\24\2\u0127\u0128\7\f\2\2\u0128/\3"+
		"\2\2\2\u0129\u012c\5\64\33\2\u012a\u012c\5\62\32\2\u012b\u0129\3\2\2\2"+
		"\u012b\u012a\3\2\2\2\u012c\61\3\2\2\2\u012d\u012e\7F\2\2\u012e\63\3\2"+
		"\2\2\u012f\u0132\5\66\34\2\u0130\u0132\58\35\2\u0131\u012f\3\2\2\2\u0131"+
		"\u0130\3\2\2\2\u0132\65\3\2\2\2\u0133\u0134\t\16\2\2\u0134\67\3\2\2\2"+
		"\u0135\u0136\t\17\2\2\u01369\3\2\2\2\u0137\u013c\5&\24\2\u0138\u0139\7"+
		"\25\2\2\u0139\u013b\5&\24\2\u013a\u0138\3\2\2\2\u013b\u013e\3\2\2\2\u013c"+
		"\u013a\3\2\2\2\u013c\u013d\3\2\2\2\u013d;\3\2\2\2\u013e\u013c\3\2\2\2"+
		"\36?CHU\\aks|\u0081\u0090\u0096\u009a\u00a0\u00ba\u00ca\u00f0\u00f2\u00f7"+
		"\u00ff\u0103\u0105\u010e\u011c\u0123\u012b\u0131\u013c";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}