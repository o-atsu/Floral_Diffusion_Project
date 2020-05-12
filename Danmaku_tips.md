# 今回の弾幕の仕様

3つのクラスで構成、それぞれを継承させて弾幕パターンを作成。
	継承については下部で説明

- Bulletクラス
	弾の種類ごとに付ける。
	弾一つの軌道を表記(ex: 直線、カーブ、ホーミング、加減速)。
	画面外に出ると勝手に非アクティブになる。

	**実装するもの:**
		- public override void Set_property(軌道設定に必要な変数)
			Barrage_generator継承クラスが呼び出して設定する

		- void Update() ...必要に応じて
			毎フレーム何かの処理をするならこの関数の中に書く
	

- Barrage_generatorクラス
	弾幕を発射するオブジェクトに付ける。
	弾一種類の発射を管理(ex: 自機狙い弾、方向ランダム弾)。
	
	**設定:**
		bullet ...発射する弾の種類
		position ...発射位置(ローカル座標)
		direction ...方向。ワールド座標での右が0°、そこから時計回り
		speed ...弾の初速
		POOL_SIZE ...(軽量化のために)あらかじめ作っておく弾数
		Generate() ...弾(1つ)を生成する関数、Attackから呼び出される

	**実装するもの:**
		- protected override void Bullet_init(ref GameObject obj)
			bulletの発射時の初期設定
			主にSet_propertyを呼び出す(Straight_generator.csを見て)


- Attackクラス
	弾幕パターンを記述(ex: n-way弾、モチーフ弾幕)

	**設定:**
		interval ...弾の発射間隔(時間)
		generators ...使用するBarrage_generator継承クラス

	**実装するもの:**
		- protected override IEnumerator shot()
			発射パターンを決める
			主に各generatorのGenerate()をinterval毎に呼び出す
		
		- void Start()関数内でコルーチンを呼び出す。(多分)(必要な場所で呼び出して)



# 説明、及び主な実装方法
	
	Vector3:型名、.x, .y, .zで各要素にアクセスできる。

	変数をインスペクタ(画面右側)で設定できるようにする:
		変数の定義を以下のようにするとできる
			- public *型* *変数名*
			- [SerializeField] private *型* *変数名*
			
	クラス継承:
		元のクラスと同じ役割を持ったクラスを生成する。
		スクリプトを生成したときはMonoBehaviourを継承してるので、そこを書き換えることで継承元を指定。
		継承元の変数、関数を持っている。
		継承元の関数を書き換える際にoverrideと書く。
			
	コルーチン:
		時間の絡んだ処理をするのに便利な関数のようなもの。任意のタイミングで中断、再開可能。
		Unityでは型をIEnumeratorにする。
		
		コルーチン呼び出し:StartCoroutine("コルーチン名")
		
		コルーチン中断:StopCoroutine("コルーチン名")

		コルーチンのみで使える関数の例:
			n秒待つ:yield return new WaitForSeconds(n);
			1フレーム待つ:yield return null;
	
	関数やコルーチンで使える関数など:
		自身の現在の座標:this.transform.position ...型:Vector3
			this.は省略可。
			末尾をposition.x, position.yとすることでx座標, y座標。
			z座標は今回使わない。
			そのまま座標に代入するとワープする

		速度:this.GetComponent<Rigidbody2D>().velocity ...型:Vector3
			this.省略可。
			x, y, zは座標同様。
			以下のように書くと、rb.velocityと書いて速度が得られる。
			'private Rigidbody2D rb;
			void Awake(){
				rb = GetComponent<Rigidbody2D>();
			}'

		角度:Quaternion等ややこしいので割愛、多分directionだけで管理できるようにする

	プレイヤーの情報を得る:
		1. GameObject型の変数を用意する(インスペクタから設定できるように)
		2. インスペクタでその変数にHierarchyにあるプレイヤーをドラッグ&ドロップ
		3. すると*変数名*.transform.positionで座標がわかる
	
	移動:
		速度や角度を変えるのがメイン。
		摩擦など物理設定を変えるのはインスペクタから。
