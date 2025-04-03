using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ChessBarrel: MonoBehaviour, IFocusable
{
    [Header("Board")]
    [SerializeField] private ChessBoard chessBoard; // 사용하는 체스 보드

    [Header("Pieces")] 
    [SerializeField] private GameObject pawn;
    [SerializeField] private GameObject rook;
    [SerializeField] private GameObject knight;
    [SerializeField] private GameObject bishop;
    [SerializeField] private GameObject king;
    [SerializeField] private GameObject queen;
    
    [Header("Materials")]
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material blackMaterial;
    
    [Header("Initialize State")]
    [Tooltip("체스판의 초기 상태를 나타내는 문자열을 입력합니다. 왼쪽 아래부터 오른쪽으로 말이 채워집니다.\n" + 
             "폰은 p, 룩은 r, 나이트는 n, 비숍은 b, 킹은 k, 퀸은 q이며 흰색 기물은 대문자, 검정색 기물은 소문자로 적어주세요.")]
    [SerializeField] private string initializeState;
    
    [Header("Clickable Piece")]
    [SerializeField] private GameObject targetPiece; // 목표 기물
    [SerializeField] private Vector2Int targetPosition; // 목표 위치

    [Header("Audio")]
    [SerializeField] private AudioClip moveClip;
    [SerializeField] private AudioClip captureClip;
    
    [Header("Events")]
    [SerializeField] private UnityEvent OnSuccess;
    
    private Vector2Int targetCurrentPosition; // 목표 기물의 현재위치
    private bool[,] canSelected; // 선택 가능 여부
    private bool isPut = false; // 목표 기물을 체스판에 뒀으면 true
    private bool isSelected = false; // 목표 기물 선택 여부
    private GameObject interactingActor; // 현재 상호작용중인 오브젝트
    private BoxCollider boxCollider; // 포커스 용 콜리전
    
    private bool isLock = true; // 잠금 여부
    private bool needUnlockAction = false;
    private AudioSource audioSource;

    void Start()
    {
        canSelected = new bool[8, 8];
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        CreatePuzzle();
    }

    private void Update()
    {
        // 이미 잠금 해제됐다면 기물의 하이라이트만 꺼주고 종료한다.
        if (!isLock)
        {
            if (needUnlockAction)
            {
                needUnlockAction = false;
                isSelected = false;
                targetPiece.GetComponent<Highlight>()?.SetHighlight(isSelected);
            }
            
            return;
        }
        
        // 기물이 선택된 상태라면 실행 종료
        if (isSelected)
        {
            return;
        }
        
        // 하이라이트를 일단 꺼주고 시작
        targetPiece?.GetComponent<Highlight>()?.SetHighlight(false);
        
        // 스크린에서 마우스 클릭 위치를 통과하는 광선 생성
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (!isPut)
            {
                // 선택한것이 기물이라면 선택 상태로 만든다.
                if (hit.collider.gameObject == targetPiece)
                {
                    targetPiece?.GetComponent<Highlight>()?.SetHighlight(true);
                }
            }
            else
            {
                if (targetCurrentPosition == chessBoard.GetSquarePositionFromCollider(hit.collider))
                {
                    targetPiece?.GetComponent<Highlight>()?.SetHighlight(true);
                }
            }
        }
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            LeftClickAction();
        }
    }

    private void LeftClickAction()
    {
        // 게임이 일시정지된 상태이거나
        // 상호작용하는 액터가 없거나
        // 이미 잠금이 해제된 상태라면 클릭 동작을 실행하지 않는다.
        if (GamePause.isPaused || interactingActor == null || !isLock)
        {
            return;
        }
        
        // 스크린에서 마우스 클릭 위치를 통과하는 광선 생성
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        // 광선이 오브젝트를 감지하면 해당 오브젝트 선택을 시도
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // 아직 기물을 두지 않았고 기물을 선택하지 않았다면
            if (!isPut && !isSelected)
            {
                // 선택한것이 기물이라면 선택 상태로 만든다.
                if (hit.collider.gameObject == targetPiece)
                {
                    SelectTargetPiece();
                }
                return;
            }
            
            Vector2Int select = chessBoard.GetSquarePositionFromCollider(hit.collider);
            // 어떠한 칸도 선택하지 않았다면 선택 상태 해제
            if(select.x == -1 && select.y == -1)
            {
                DeSelectTargetPiece();
            }
            else // 칸을 선택했다면
            {
                // 기물을 선택한 상태가 아니고 선택한 칸이 기물이 있는 칸이면 선택 상태로 변경
                if (!isSelected)
                {
                    if (targetCurrentPosition == select)
                    {
                        SelectTargetPiece();
                    }
                }
                else
                {
                    // 기물을 선택한 상태인데 선택이 가능한 칸이면 해당 칸으로 기물 이동
                    if (canSelected[select.x, select.y])
                    {
                        targetPiece.transform.position = chessBoard.GetSquareWorldPosition(select.x, select.y);
                        targetPiece.transform.rotation = chessBoard.transform.rotation;
                        targetCurrentPosition = select;
                        targetPiece.GetComponent<Highlight>()?.SetHighlight(false);
                        isSelected = false;
                        audioSource.PlayOneShot(moveClip);
                        
                        // 기물을 처음 놓았다면 isPut을 true로 바꾸고 기물의 콜리전을 끈다.
                        if (!isPut)
                        {
                            isPut = true;
                            targetPiece.GetComponent<Collider>().enabled = false;
                        }
                        
                        // 기물이 목표 지점에 놓였다면 잠금 상태 해제
                        if (targetCurrentPosition == targetPosition)
                        {
                            isLock = false;
                            needUnlockAction = false;
                            OnSuccess?.Invoke();
                        }
                    }
                    else // 기물을 선택한 상태인데 선택 기능한 칸이 아니면 선택 상태 해제
                    {
                        DeSelectTargetPiece();
                    }
                }
            }
        }
    }

    private void SelectTargetPiece()
    {
        if (!isSelected)
        {
            isSelected = true;
            targetPiece.GetComponent<Highlight>()?.SetHighlight(true);
            targetPiece.transform.localPosition += 0.01f * Vector3.up;
            audioSource.PlayOneShot(captureClip);
        }
    }
    
    private void DeSelectTargetPiece()
    {
        if (isSelected)
        {
            isSelected = false;
            targetPiece.GetComponent<Highlight>()?.SetHighlight(false);
            audioSource.PlayOneShot(moveClip);
        }
    }
    
    // player랑 상호작용 시작
    public void Focus(GameObject actor)
    {
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "Interaction Camera", 
            beforeSwitch: () => PlayerLoading.PlayerSetStop(),
            afterSwitch: () => PlayerLoading.SetEnableInput(true));
            //afterSwitch // 마우스 클릭만 활성화 하는 매서드
        
        CursorLocker.instance.UnlockCursor();
        interactingActor = actor;
        boxCollider.enabled = false;
    }
    
    // player랑 상호작용 끝
    public void UnFocus(GameObject actor)
    {
        isSelected = false;
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "Player Camera",
            beforeSwitch: () => PlayerLoading.SetEnableInput(false),
            afterSwitch: () => PlayerLoading.PlayerSetStart());
        
        CursorLocker.instance.LockCursor();
        interactingActor = null;
        boxCollider.enabled = true;
    }
    
    // initializeState에 맞춰서 기물 배치
    private void CreatePuzzle()
    {
        // 조작할 수 있는 기물을 제외한 나머지 기물 먼조 스폰
        for (int h = 0; h < chessBoard.heightCount; h++)
        {
            for (int w = 0; w < chessBoard.widthCount; w++)
            {
                int index = h * chessBoard.widthCount + w;
                if (initializeState.Length <= index)
                {
                    continue;
                }

                SpawnPiece(initializeState[index], w, h);
            }
        }
    }
    
    // 기물 스폰
    private void SpawnPiece(char pieceCode, int x, int y)
    {
        Vector3 spawnPosition = chessBoard.GetSquareWorldPosition(x, y);
        GameObject spawnedPiece = null;
        
        switch (pieceCode)
        {
            case 'P':
            case 'p':
                spawnedPiece = Instantiate(pawn, spawnPosition, chessBoard.transform.rotation);
                break;
            case 'R':
            case 'r':
                spawnedPiece = Instantiate(rook, spawnPosition, chessBoard.transform.rotation);
                break;
            case 'N':
            case 'n':
                spawnedPiece = Instantiate(knight, spawnPosition, chessBoard.transform.rotation);
                break;
            case 'B':
            case 'b':
                spawnedPiece = Instantiate(bishop, spawnPosition, chessBoard.transform.rotation);
                break;
            case 'Q':
            case 'q':
                spawnedPiece = Instantiate(queen, spawnPosition, chessBoard.transform.rotation);
                break;
            case 'K':
            case 'k':
                spawnedPiece = Instantiate(king, spawnPosition, chessBoard.transform.rotation);
                break;
        }
        
        if (spawnedPiece != null)
        {
            Renderer renderer = spawnedPiece.GetComponent<Renderer>();
            if ('A' <= pieceCode && pieceCode <= 'Z')
            {
                renderer.material = whiteMaterial;
            }
            else
            {
                renderer.material = blackMaterial;
            }

            canSelected[x, y] = false;
        }
        else
        {
            canSelected[x, y] = true;
        }
    }
}