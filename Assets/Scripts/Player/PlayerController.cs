using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {

	private int position = 1;
	private int maxPosition = 2, minPosition = 0;
	private float inputDelay = 0.2f, timer;
	private Vector3 initialPosition;
	private Quaternion initialRotation;

	public float speed = 1;
	private float startTime;
	private float journeyLength;

	private Vector2 fp, lp;

	private Animator animator;
	private bool moveable = true;
	public Animator marker;
	private int buttonPressDirection = 0;
	public ControlScheme controlScheme;

	public GameObject[] playerModels;

	private void Awake() {
		animator = GetComponent<Animator> ();
		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}

	private void Start() {
		playerModels [PlayerPrefs.GetInt ("SelectedCar")].SetActive (true);
	}

	private void Update() {
		timer += Time.deltaTime;

		if (moveable) {
			//PC
			if (Input.GetAxis ("Horizontal") < 0 && position < maxPosition && timer >= inputDelay) {
				position++;
				timer = 0;
				SetLerpValues ();
				animator.SetTrigger ("OnSteerLeft");
			} else if (Input.GetAxis ("Horizontal") > 0 && position > minPosition && timer >= inputDelay) {
				position--;
				timer = 0;
				SetLerpValues ();
				animator.SetTrigger ("OnSteerRight");
			}

			//Mobile (swipe)
			if (Input.touchCount > 0 && controlScheme.GetControls() != 1) {
				Touch touch = Input.GetTouch (0);
				if (touch.phase == TouchPhase.Began) {
					fp = touch.position;
					lp = touch.position;
				}
				if (touch.phase == TouchPhase.Moved) {
					lp = touch.position;
				}
				if (touch.phase == TouchPhase.Ended) {
					if ((fp.x - lp.x) > 50 && position < maxPosition) {
						position++;
						SetLerpValues ();
						animator.SetTrigger ("OnSteerLeft");
					} else if ((fp.x - lp.x) < -50 && position > minPosition) {
						position--;
						SetLerpValues ();
						animator.SetTrigger ("OnSteerRight");
					}
				}
			}

			//Mobile (tap)
			if (buttonPressDirection < 0 && position < maxPosition && timer >= inputDelay) {
				position++;
				timer = 0;
				SetLerpValues ();
				animator.SetTrigger ("OnSteerLeft");
			} else if (buttonPressDirection > 0 && position > minPosition && timer >= inputDelay) {
				position--;
				timer = 0;
				SetLerpValues ();
				animator.SetTrigger ("OnSteerRight");
			}
		}

		if (journeyLength != 0) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(transform.position, new Vector3(1.5f * position, 0.25f, 0), fracJourney);
		}
	}

	private void SetLerpValues() {
		startTime = Time.time;
		journeyLength = Vector3.Distance(transform.position, new Vector3(1.5f * position, 0.25f, 0));
	}

	public void Reset() {
		position = 1;
		maxPosition = 2;
		transform.position = initialPosition;
		transform.rotation = initialRotation;
		animator.SetTrigger ("OnIdle");
		moveable = true;
		marker.gameObject.SetActive (true);
		buttonPressDirection = 0;

		for (int i = 0; i < playerModels.Length; i++) {
			playerModels [i].SetActive (false);
		}
		playerModels [PlayerPrefs.GetInt ("SelectedCar")].SetActive (true);
	}

	public void IncreaseMaxPosition() {
		maxPosition++;
	}

	public void DecreaseMaxPosition() {
		if (position == maxPosition) {
			position--;
			SetLerpValues ();
			animator.SetTrigger ("OnSteerRight");
		}
		maxPosition--;
	}

	public void SetMoveableOn() {
		moveable = true;
		marker.gameObject.SetActive (true);
		marker.Play ("Idle", 0, 0f);
	}

	public void SetMoveableOff() {
		moveable = false;
		marker.gameObject.SetActive (false);
	}

	private void OnEnable() {
		Reset ();
	}

	public void OnMove(int direction) {
		buttonPressDirection = direction;
	}
}
