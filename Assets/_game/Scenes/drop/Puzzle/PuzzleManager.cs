using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzlePiecePrefab;
    public Transform piecesPanel;
    public Transform puzzleArea;
    public Image puzzleHintImage; // 添加提示图像
    public Text timerText;
    public Text finalTimeText;
    public Button backButton;
    public Button finishButton;

    private List<GameObject> pieces;
    private float timer;
    private bool isGameFinished;

    private void Start()
    {
        InitializePuzzle();
        backButton.onClick.AddListener(OnBackButtonClicked);
        finishButton.onClick.AddListener(OnFinishButtonClicked);
        finalTimeText.gameObject.SetActive(false);
        puzzleHintImage.gameObject.SetActive(true); // 开始时显示提示图像
    }

    private void Update()
    {
        if (!isGameFinished)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2") + "s";
        }
    }

    private void InitializePuzzle()
    {
        pieces = new List<GameObject>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Puzzle1"); // 假设所有切割好的图像块都在Sprites文件夹中

        float pieceSize = 100f; // 假设每个拼图碎片的大小为100x100像素

        for (int i = 0; i < sprites.Length; i++)
        {
            GameObject piece = Instantiate(puzzlePiecePrefab, piecesPanel);
            piece.GetComponent<SpriteRenderer>().sprite = sprites[i];
            piece.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * pieceSize); // 初始化位置
            piece.GetComponent<RectTransform>().localScale = Vector3.one; // 确保缩放正确

            // 根据拼图碎片的名字确定其正确位置
            string pieceName = sprites[i].name;
            int pieceIndex = int.Parse(pieceName) - 1; // 假设名字从1开始
            piece.GetComponent<PuzzlePiece>().correctPosition = new Vector2((pieceIndex % 5) * pieceSize, -(pieceIndex / 5) * pieceSize); // 计算正确位置

            Debug.Log($"Created piece {pieceName} with sprite {sprites[i].name} at correct position {piece.GetComponent<PuzzlePiece>().correctPosition}");
            pieces.Add(piece);
        }
    }

    public void OnPuzzlePiecePlaced(GameObject piece)
    {
        pieces.Remove(piece);
        if (pieces.Count == 0)
        {
            isGameFinished = true;
            ShowFinalTime();
            puzzleHintImage.gameObject.SetActive(false); // 拼图完成后隐藏提示图像
        }
    }

    private void ShowFinalTime()
    {
        finalTimeText.gameObject.SetActive(true);
        finalTimeText.text = "Final Time: " + timer.ToString("F2") + "s";
    }

    public void OnBackButtonClicked()
    {
        // 处理返回按钮逻辑
    }

    public void OnFinishButtonClicked()
    {
        if (isGameFinished)
        {
            // 处理完成按钮逻辑
        }
    }
}
