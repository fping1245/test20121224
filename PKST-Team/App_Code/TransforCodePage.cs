//---------------------------------------------------------------------------- 
//專案名稱	公用函數
//程式功能	繁簡轉換處理
//---------------------------------------------------------------------------- 
using System.IO;
using System.Text;

#region TransforCodePage 繁簡轉換基本物件
public class TransforCodePage
{
	// 簡體對映字串
	string Str936 = "皑蔼碍爱袄奥坝罢摆败颁办绊帮绑镑谤剥饱宝报鲍辈贝钡狈备惫绷笔毕毙币闭边编贬变辩辫标鳖别瘪濒滨宾摈饼并拨钵铂驳卜补财参蚕残惭惨灿苍舱仓沧厕侧册测层诧搀掺蝉馋谗缠铲产阐颤场尝长偿肠厂畅钞车彻尘陈衬撑称惩诚骋痴迟驰耻齿炽冲虫宠畴踌筹绸丑橱厨锄雏础储触处传疮闯创锤纯绰辞词赐聪葱囱从丛凑蹿窜错达带贷担单郸掸胆惮诞弹当挡党荡档捣岛祷导盗灯邓敌涤递缔颠点垫电淀钓调谍叠钉顶锭订丢东动栋冻斗犊独读赌镀锻断缎兑队对吨顿钝夺堕鹅额讹恶饿儿尔饵贰发罚阀珐矾钒烦范贩饭访纺飞诽废费纷坟奋愤粪丰枫锋风疯冯缝讽凤肤辐抚辅赋复负讣妇缚该钙盖干赶秆赣冈刚钢纲岗镐搁鸽阁铬个给龚宫巩贡钩沟构购够蛊顾剐挂关观馆惯贯广规硅归龟闺轨诡柜贵刽辊滚锅国过骇韩汉号阂鹤贺横轰鸿红后壶护沪户哗华画划话怀坏欢环还缓换唤痪焕涣黄谎挥辉毁贿秽会烩汇讳诲绘荤浑伙获货祸击机积饥迹讥鸡绩缉极辑级挤几蓟剂济计记际继纪夹荚颊贾钾价驾歼监坚笺间艰缄茧检碱硷拣捡简俭减荐槛鉴践贱见键舰剑饯渐溅涧将浆蒋桨奖讲酱胶浇骄娇搅铰矫侥脚饺缴绞轿较阶节杰洁结诫届紧锦仅谨进晋烬尽劲荆茎鲸惊经颈静镜径痉竞净纠厩旧驹举据锯惧剧鹃绢觉决诀绝钧军骏开凯颗壳课垦恳抠库裤夸块侩宽矿旷况亏岿窥馈溃扩阔蜡腊莱来赖蓝栏拦篮阑兰澜谰揽览懒缆烂滥捞劳涝乐镭垒类泪篱离里鲤礼丽厉励砾历沥隶俩联莲连镰怜涟帘敛脸链恋炼练粮凉两辆谅疗辽镣猎临邻鳞凛赁龄铃凌灵岭领馏刘龙聋咙笼垄拢陇楼娄搂篓芦卢颅庐炉掳卤虏鲁赂禄录陆驴吕铝侣屡缕虑滤绿峦挛孪滦乱抡轮伦仑沦纶论萝罗逻锣箩骡骆络妈玛码蚂马骂吗买麦卖迈脉瞒馒蛮满谩猫锚铆贸么霉没镁门闷们锰梦谜弥觅幂绵缅庙灭悯闽鸣铭谬谋亩钠纳难挠脑恼闹馁内拟腻撵捻酿鸟聂啮镊镍柠狞宁拧泞钮纽脓浓农疟诺欧鸥殴呕沤盘庞抛赔喷鹏骗飘频贫苹凭评泼颇扑铺朴谱栖凄脐齐骑岂启气弃讫牵扦铅迁签谦钱钳潜浅谴堑枪呛墙蔷强抢锹桥乔侨翘窍窃钦亲寝轻氢倾顷请庆琼穷趋区躯驱龋颧权劝却鹊确让饶扰绕热韧认纫荣绒软锐闰润洒萨鳃赛叁伞丧骚扫涩杀纱筛晒删闪陕赡缮伤赏烧绍赊摄慑设绅审婶肾渗声绳胜圣师狮湿诗尸时蚀实识驶势适释饰视试寿兽枢输书赎属术树竖数帅双谁税顺说硕烁丝饲耸怂颂讼诵擞苏诉肃虽随绥岁孙损笋缩琐锁獭挞态摊贪瘫滩坛谭谈叹汤烫涛绦讨腾誊锑题体屉条贴铁厅听烃铜统头秃图涂团颓蜕脱鸵驮驼椭洼袜弯湾顽万网韦违围为潍维苇伟伪纬谓卫温闻纹稳问瓮挝蜗涡窝卧呜钨乌污诬无芜吴坞雾务误锡牺袭习铣戏细虾辖峡侠狭厦吓鲜纤咸贤衔闲显险现献县馅羡宪线厢镶乡详响项萧嚣销晓啸蝎协挟携胁谐写泻谢锌衅兴汹锈绣虚嘘须许叙绪续轩悬选癣绚学勋询寻驯训讯逊压鸦鸭哑亚讶阉烟盐严颜阎艳厌砚彦谚验鸯杨扬疡阳痒养样瑶摇尧遥窑谣药爷页业叶医铱颐遗仪蚁艺亿忆义诣议谊译异绎荫阴银饮隐樱婴鹰应缨莹萤营荧蝇赢颖哟拥佣痈踊咏涌优忧邮铀犹诱舆鱼渔娱与屿语吁御狱誉预驭鸳渊辕园员圆缘远愿约跃钥岳粤悦阅云郧匀陨运蕴酝晕韵杂灾载攒暂赞赃脏凿枣责择则泽贼赠扎札轧铡闸栅诈斋债毡盏斩辗崭栈战绽张涨帐账胀赵蛰辙锗这贞针侦诊镇阵挣睁狰争帧郑证织职执纸挚掷帜质滞钟终种肿众诌轴皱昼骤猪诸诛烛瞩嘱贮铸筑驻专砖转赚桩庄装妆壮状锥赘坠缀谆着浊兹资渍踪综总纵邹诅组钻亘芈啬厍厣靥赝匦匮赜刭刿剀伛伥伧伫侪侬俦俨俪俣偾偬偻傥傧傩佥籴黉冁凫兖衮亵脔禀冢讦讧讪讴讵讷诂诃诋诏诒诓诔诖诘诙诜诟诠诤诨诩诮诰诳诶诹诼诿谀谂谄谇谌谏谑谒谔谕谖谙谛谘谝谟谠谡谥谧谪谫谮谯谲谳谵谶卺陉陧邝邬邺郏郐郓郦刍奂劢巯垩圹坜垆垭垲埘埚埙芗苈苋苌苁苎茏茑茔茕荛荜荞荟荠荦荥荩荪荭莳莴莅莸莺萦蒇蒉蒌蓦蓠蓣蔹蔺蕲薮藓奁尴扪抟挢掴掼揿摅撄撷撸撺叽呒呓呖呗咛哒哓哔哕哙哜哝唛唠唢啧啭喽喾嗫嗳辔嘤噜囵帏帱帻帼岖岘岚峄峤峥崂崃嵘嵛嵝巅徕犷狯狲猃猡猕饧饨饩饪饫饬饴饷饽馀馄馊馍馐馑馔庑赓廪忏怃怄忾怅怆怿恸恹恻恺恽悭惬愠愦懔闩闫闱闳闵闶闼闾阃阄阆阈阊阌阍阏阒阕阖阗阙阚沣沩泷泸泺泾浃浈浍浏浒浔涞涠渎渑渖渌溆滟滠滢滗潆潇潋潴濑灏骞迩迳逦屦弪妩妪妫姗娅娆娈娲娴婵媪嫒嫔嫱嬷驵驷驸驺驿驽骀骁骅骈骊骐骒骓骖骘骛骜骝骟骠骢骣骥骧纡纣纥纨纩纭纰纾绀绁绂绉绋绌绗绛绠绡绨绫绮绯绲缍绶绺绻绾缁缂缃缇缈缋缌缏缑缒缗缙缜缛缟缡缢缣缤缥缦缧缪缫缬缭缯缱缲缳缵玑玮珏珑顼玺珲琏瑷璎璇瓒韪韫韬杩枥枨枞枭栉栊栌栀栎柽桠桡桢桤桦桧栾棂椟椠椤椁榄榇榈榉槟槠樯橥橹橼檐檩殁殇殒殓殚殡轫轭轲轳轵轶轸轹轺轼轾辁辂辄辇辋辍辎辏辘辚戋戗戬瓯昙晔晖暧贲贳贶贻贽赀赅赆赈赉赇赕赙觇觊觋觌觎觏觐觑毵氇氩氲牍胧胪胫脍脶腌腽膑欤飑飒飓飕飙毂齑斓炀炜炖烨焖焘祢祯禅怼悫愍懑戆沓泶矶砀砗砺砻硖硗碛碜龛睐睑畲罴羁钆钇钋钊钌钍钏钐钔钗钕钛钣钤钫钪钭钬钯钰钲钴钶钸钹钺钼钽钿铄铈铉铊铋铌铍铎铐铑铒铕铖铗铙铛铟铠铢铤铥铧铨铪铩铫铮铯铳铴铵铷铹铼铽铿锂锆锇锉锊锒锓锔锕锖锛锞锟锢锩锬锱锲锴锶锷锸锼锾镂锵镆镉镌镏镒镓镔镖镗镘镙镛镞镟镝镡镤镦镧镨镪镫镬镯镱镳锺穑鸠鸢鸨鸩鸪鸫鸬鸲鸱鸶鸸鸷鸹鸺鸾鹁鹂鹄鹆鹇鹈鹉鹌鹎鹑鹕鹗鹞鹣鹦鹧鹨鹩鹪鹫鹬鹭鹳疖疠痨痫瘅瘗瘿瘾癞癫窦窭裆裢裣裥褛褴襁皲耧聍聩顸颀颃颉颌颏颔颚颛颞颟颡颢颦虬虮虿蚬蚝蛎蛏蛱蛲蛳蛴蝈蝾蝼罂笃笕笾筚筝箦箧箨箪箫篑簖籁舣舻袅羟糁絷麸趱酽酾鹾趸跄跖跞跷跸跹跻踬踯蹑蹒蹰躏躜觞觯靓雳霁霭龀龃龅龆龇龈龉龊龌黾鼋鼍隽雠銮錾鱿鲂鲅鲈稣鲋鲎鲐鲒鲔鲕鲚鲛鲞鲟鲠鲡鲢鲣鲥鲦鲧鲨鲩鲫鲭鲮鲰鲱鲲鲳鲵鲶鲷鲻鲽鳄鳅鳆鳇鳌鳍鳎鳏鳐鳓鳔鳕鳗鳜鳝鳟鳢鞑鞯鹘髅髋髌魇魉飨餍鬓黩黪鼹";

	// 繁體對映字串
	string Str950 = "皚藹礙愛襖奧壩罷擺敗頒辦絆幫綁鎊謗剝飽寶報鮑輩貝鋇狽備憊繃筆畢斃幣閉邊編貶變辯辮標鱉別癟瀕濱賓擯餅並撥缽鉑駁蔔補財參蠶殘慚慘燦蒼艙倉滄廁側冊測層詫攙摻蟬饞讒纏鏟產闡顫場嘗長償腸廠暢鈔車徹塵陳襯撐稱懲誠騁癡遲馳恥齒熾沖蟲寵疇躊籌綢醜櫥廚鋤雛礎儲觸處傳瘡闖創錘純綽辭詞賜聰蔥囪從叢湊躥竄錯達帶貸擔單鄲撣膽憚誕彈當擋黨蕩檔搗島禱導盜燈鄧敵滌遞締顛點墊電澱釣調諜疊釘頂錠訂丟東動棟凍鬥犢獨讀賭鍍鍛斷緞兌隊對噸頓鈍奪墮鵝額訛惡餓兒爾餌貳發罰閥琺礬釩煩範販飯訪紡飛誹廢費紛墳奮憤糞豐楓鋒風瘋馮縫諷鳳膚輻撫輔賦複負訃婦縛該鈣蓋幹趕稈贛岡剛鋼綱崗鎬擱鴿閣鉻個給龔宮鞏貢鉤溝構購夠蠱顧剮掛關觀館慣貫廣規矽歸龜閨軌詭櫃貴劊輥滾鍋國過駭韓漢號閡鶴賀橫轟鴻紅後壺護滬戶嘩華畫劃話懷壞歡環還緩換喚瘓煥渙黃謊揮輝毀賄穢會燴彙諱誨繪葷渾夥獲貨禍擊機積饑跡譏雞績緝極輯級擠幾薊劑濟計記際繼紀夾莢頰賈鉀價駕殲監堅箋間艱緘繭檢堿鹼揀撿簡儉減薦檻鑒踐賤見鍵艦劍餞漸濺澗將漿蔣槳獎講醬膠澆驕嬌攪鉸矯僥腳餃繳絞轎較階節傑潔結誡屆緊錦僅謹進晉燼盡勁荊莖鯨驚經頸靜鏡徑痙競淨糾廄舊駒舉據鋸懼劇鵑絹覺決訣絕鈞軍駿開凱顆殼課墾懇摳庫褲誇塊儈寬礦曠況虧巋窺饋潰擴闊蠟臘萊來賴藍欄攔籃闌蘭瀾讕攬覽懶纜爛濫撈勞澇樂鐳壘類淚籬離裏鯉禮麗厲勵礫曆瀝隸倆聯蓮連鐮憐漣簾斂臉鏈戀煉練糧涼兩輛諒療遼鐐獵臨鄰鱗凜賃齡鈴淩靈嶺領餾劉龍聾嚨籠壟攏隴樓婁摟簍蘆盧顱廬爐擄鹵虜魯賂祿錄陸驢呂鋁侶屢縷慮濾綠巒攣孿灤亂掄輪倫侖淪綸論蘿羅邏鑼籮騾駱絡媽瑪碼螞馬罵嗎買麥賣邁脈瞞饅蠻滿謾貓錨鉚貿麼黴沒鎂門悶們錳夢謎彌覓冪綿緬廟滅憫閩鳴銘謬謀畝鈉納難撓腦惱鬧餒內擬膩攆撚釀鳥聶齧鑷鎳檸獰甯擰濘鈕紐膿濃農瘧諾歐鷗毆嘔漚盤龐拋賠噴鵬騙飄頻貧蘋憑評潑頗撲鋪樸譜棲淒臍齊騎豈啟氣棄訖牽扡鉛遷簽謙錢鉗潛淺譴塹槍嗆牆薔強搶鍬橋喬僑翹竅竊欽親寢輕氫傾頃請慶瓊窮趨區軀驅齲顴權勸卻鵲確讓饒擾繞熱韌認紉榮絨軟銳閏潤灑薩鰓賽三傘喪騷掃澀殺紗篩曬刪閃陝贍繕傷賞燒紹賒攝懾設紳審嬸腎滲聲繩勝聖師獅濕詩屍時蝕實識駛勢適釋飾視試壽獸樞輸書贖屬術樹豎數帥雙誰稅順說碩爍絲飼聳慫頌訟誦擻蘇訴肅雖隨綏歲孫損筍縮瑣鎖獺撻態攤貪癱灘壇譚談歎湯燙濤絛討騰謄銻題體屜條貼鐵廳聽烴銅統頭禿圖塗團頹蛻脫鴕馱駝橢窪襪彎灣頑萬網韋違圍為濰維葦偉偽緯謂衛溫聞紋穩問甕撾蝸渦窩臥嗚鎢烏汙誣無蕪吳塢霧務誤錫犧襲習銑戲細蝦轄峽俠狹廈嚇鮮纖鹹賢銜閑顯險現獻縣餡羨憲線廂鑲鄉詳響項蕭囂銷曉嘯蠍協挾攜脅諧寫瀉謝鋅釁興洶鏽繡虛噓須許敘緒續軒懸選癬絢學勳詢尋馴訓訊遜壓鴉鴨啞亞訝閹煙鹽嚴顏閻豔厭硯彥諺驗鴦楊揚瘍陽癢養樣瑤搖堯遙窯謠藥爺頁業葉醫銥頤遺儀蟻藝億憶義詣議誼譯異繹蔭陰銀飲隱櫻嬰鷹應纓瑩螢營熒蠅贏穎喲擁傭癰踴詠湧優憂郵鈾猶誘輿魚漁娛與嶼語籲禦獄譽預馭鴛淵轅園員圓緣遠願約躍鑰嶽粵悅閱雲鄖勻隕運蘊醞暈韻雜災載攢暫贊贓髒鑿棗責擇則澤賊贈紮劄軋鍘閘柵詐齋債氈盞斬輾嶄棧戰綻張漲帳賬脹趙蟄轍鍺這貞針偵診鎮陣掙睜猙爭幀鄭證織職執紙摯擲幟質滯鍾終種腫眾謅軸皺晝驟豬諸誅燭矚囑貯鑄築駐專磚轉賺樁莊裝妝壯狀錐贅墜綴諄著濁茲資漬蹤綜總縱鄒詛組鑽亙羋嗇厙厴靨贗匭匱賾剄劌剴傴倀傖佇儕儂儔儼儷俁僨傯僂儻儐儺僉糴黌囅鳧兗袞褻臠稟塚訐訌訕謳詎訥詁訶詆詔詒誆誄詿詰詼詵詬詮諍諢詡誚誥誑誒諏諑諉諛諗諂誶諶諫謔謁諤諭諼諳諦諮諞謨讜謖諡謐謫譾譖譙譎讞譫讖巹陘隉鄺鄔鄴郟鄶鄆酈芻奐勱巰堊壙壢壚埡塏塒堝塤薌藶莧萇蓯苧蘢蔦塋煢蕘蓽蕎薈薺犖滎藎蓀葒蒔萵蒞蕕鶯縈蕆蕢蔞驀蘺蕷蘞藺蘄藪蘚奩尷捫摶撟摑摜撳攄攖擷擼攛嘰嘸囈嚦唄嚀噠嘵嗶噦噲嚌噥嘜嘮嗩嘖囀嘍嚳囁噯轡嚶嚕圇幃幬幘幗嶇峴嵐嶧嶠崢嶗崍嶸崳嶁巔徠獷獪猻獫玀獼餳飩餼飪飫飭飴餉餑餘餛餿饃饈饉饌廡賡廩懺憮慪愾悵愴懌慟懨惻愷惲慳愜慍憒懍閂閆闈閎閔閌闥閭閫鬮閬閾閶閿閽閼闃闋闔闐闕闞灃溈瀧瀘濼涇浹湞澮瀏滸潯淶潿瀆澠瀋淥漵灩灄瀅潷瀠瀟瀲瀦瀨灝騫邇逕邐屨弳嫵嫗媯姍婭嬈孌媧嫻嬋媼嬡嬪嬙嬤駔駟駙騶驛駑駘驍驊駢驪騏騍騅驂騭騖驁騮騸驃驄驏驥驤紆紂紇紈纊紜紕紓紺絏紱縐紼絀絎絳綆綃綈綾綺緋緄綞綬綹綣綰緇緙緗緹緲繢緦緶緱縋緡縉縝縟縞縭縊縑繽縹縵縲繆繅纈繚繒繾繰繯纘璣瑋玨瓏頊璽琿璉璦瓔璿瓚韙韞韜榪櫪棖樅梟櫛櫳櫨梔櫟檉椏橈楨榿樺檜欒欞櫝槧欏槨欖櫬櫚櫸檳櫧檣櫫櫓櫞簷檁歿殤殞殮殫殯軔軛軻轤軹軼軫轢軺軾輊輇輅輒輦輞輟輜輳轆轔戔戧戩甌曇曄暉曖賁貰貺貽贄貲賅贐賑賚賕賧賻覘覬覡覿覦覯覲覷毿氌氬氳牘朧臚脛膾腡醃膃臏歟颮颯颶颼飆轂齏斕煬煒燉燁燜燾禰禎禪懟愨湣懣戇遝澩磯碭硨礪礱硤磽磧磣龕睞瞼佘羆羈釓釔釙釗釕釷釧釤鍆釵釹鈦鈑鈐鈁鈧鈄鈥鈀鈺鉦鈷鈳鈽鈸鉞鉬鉭鈿鑠鈰鉉鉈鉍鈮鈹鐸銬銠鉺銪鋮鋏鐃鐺銦鎧銖鋌銩鏵銓鉿鎩銚錚銫銃鐋銨銣鐒錸鋱鏗鋰鋯鋨銼鋝鋃鋟鋦錒錆錛錁錕錮錈錟錙鍥鍇鍶鍔鍤鎪鍰鏤鏘鏌鎘鐫鎦鎰鎵鑌鏢鏜鏝鏍鏞鏃鏇鏑鐔鏷鐓鑭鐠鏹鐙鑊鐲鐿鑣鍾穡鳩鳶鴇鴆鴣鶇鸕鴝鴟鷥鴯鷙鴰鵂鸞鵓鸝鵠鵒鷳鵜鵡鵪鵯鶉鶘鶚鷂鶼鸚鷓鷚鷯鷦鷲鷸鷺鸛癤癘癆癇癉瘞癭癮癩癲竇窶襠褳襝襇褸襤繈皸耬聹聵頇頎頏頡頜頦頷顎顓顳顢顙顥顰虯蟣蠆蜆蠔蠣蟶蛺蟯螄蠐蟈蠑螻罌篤筧籩篳箏簀篋籜簞簫簣籪籟艤艫嫋羥糝縶麩趲釅釃鹺躉蹌蹠躒蹺蹕躚躋躓躑躡蹣躕躪躦觴觶靚靂霽靄齔齟齙齠齜齦齬齪齷黽黿鼉雋讎鑾鏨魷魴鮁鱸穌鮒鱟鮐鮚鮪鮞鱭鮫鯗鱘鯁鱺鰱鰹鰣鰷鯀鯊鯇鯽鯖鯪鯫鯡鯤鯧鯢鯰鯛鯔鰈鱷鰍鰒鰉鼇鰭鰨鰥鰩鰳鰾鱈鰻鱖鱔鱒鱧韃韉鶻髏髖髕魘魎饗饜鬢黷黲鼴";

	public string toBig5(string mstr) {
		string str = "";
		foreach (char mchar in mstr) {
			if (Str936.IndexOf(mchar) > 0)
				str += Str950.Substring(Str936.IndexOf(mchar),1);
			else
				str += mchar;
		}
		return str;
	}

	public string toGB(string mstr) {
		string str  = "";
		foreach (char mchar in mstr)
		{
			if (Str950.IndexOf(mchar) > 0)
				str += Str936.Substring(Str950.IndexOf(mchar),1);
			else
				str += mchar;
		}
		return str;
	}
}
#endregion

#region Big5ToGBFilter 網頁繁轉簡應用物件
public class Big5ToGBFilter : Stream
{
	private Stream strSink;
	private long lngPosition;

	public Big5ToGBFilter(Stream sink)
	{
		strSink = sink;
	}

	public override bool CanRead {
		get { return true; }
	}

	public override bool CanSeek {
		get { return true; }
	}

	public override bool CanWrite {
		get { return true; }
	}

	public override long Length {
		get { return 0; }
	}

	public override long Position {
		get { return lngPosition; }
		set { lngPosition = value; }
	}

	public override long Seek(long offset, System.IO.SeekOrigin direction)
	{
		return strSink.Seek(offset, direction);
	}

	public override void SetLength(long length)
	{
		strSink.SetLength(length);
	}

	public override void Close()
	{
		strSink.Close();
	}

	public override void Flush()
	{
		strSink.Flush();
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		return strSink.Read(buffer, offset, count);
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		byte[] data = new byte[count];
		string in_data = "";

		TransforCodePage tfcp = new TransforCodePage();

		// 取得網頁資料
		System.Buffer.BlockCopy(buffer, offset, data, 0, count);
		in_data = Encoding.UTF8.GetString(data).ToString();
		
		// 轉換成簡體文字
		in_data = tfcp.toGB(in_data);

		data = Encoding.UTF8.GetBytes(in_data);

		// 存回網頁資料
		strSink.Write(data, 0, count);
	}
}
#endregion

#region GBToBig5Filter 網頁簡轉繁應用物件
public class GBToBig5Filter : Stream
{
	private Stream strSink;
	private long lngPosition;

	public GBToBig5Filter(Stream sink)
	{
		strSink = sink;
	}

	public override bool CanRead {
		get { return true; }
	}

	public override bool CanSeek {
		get { return true; }
	}

	public override bool CanWrite {
		get { return true; }
	}

	public override long Length {
		get { return 0; }
	}

	public override long Position {
		get { return lngPosition; }
		set { lngPosition = value; }
	}

	public override long Seek(long offset, System.IO.SeekOrigin direction)
	{
		return strSink.Seek(offset, direction);
	}

	public override void SetLength(long length)
	{
		strSink.SetLength(length);
	}

	public override void Close()
	{
		strSink.Close();
	}

	public override void Flush()
	{
		strSink.Flush();
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		return strSink.Read(buffer, offset, count);
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		byte[] data = new byte[count];

		TransforCodePage tfcp = new TransforCodePage();

		// 取得網頁資料
		System.Buffer.BlockCopy(buffer, offset, data, 0, count);
		string in_data = Encoding.UTF8.GetString(data).ToString();

		// 轉成繁體文字
		in_data = tfcp.toBig5(in_data);
		data = Encoding.UTF8.GetBytes(in_data);

		// 存回網頁資料
		strSink.Write(data, 0, count);
	}
}
#endregion
