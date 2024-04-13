using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlidingPuzzleHandler : MonoBehaviour
{
  [SerializeField] private Transform gameTransform;
  [SerializeField] private Transform piecePrefab;
  [SerializeField] private GameObject door;
    public bool win = false;

  private List<Transform> pieces;
  private int emptyLocation;
  [SerializeField] int size = 4;
    private bool shuffling = false;
    private bool firstTime = true;

    // Create the game setup with size x size pieces.
    private void CreateGamePieces(float gapThickness) {
    // This is the width of each tile.
    float width = 1 / (float)size;
    for (int row = 0; row < size; row++) 
        {
      for (int col = 0; col < size; col++) 
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                // Pieces will be in a game board going from -1 to +1.
                piece.localPosition = new Vector3((-1 + (2 * width * col) + width) * 750,
                                                  +(1 - (2 * width * row) - width) * 750,
                                                  0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";
                // We want an empty space in the bottom right.
                if ((row == size - 1) && (col == size - 1)) 
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                } 
                else {
                    // We want to map the UV coordinates appropriately, they are 0->1.
                    float gap = gapThickness / 2;
                    RawImage image = piece.GetComponent<RawImage>();
                    //Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Rect[] uv = new Rect[4];
                    // UV coord order: (0, 1), (1, 1), (0, 0), (1, 0)
                    uv[0] = new Rect((width * (col+1)) + gap, 1 - ((width * (row)) - gap), width, width);
                    image.uvRect = uv[0];
                    uv[1] = new Rect((width * col) - gap, 1 - ((width * (row)) - gap), width, width);
                    image.uvRect = uv[1];
                    uv[2] = new Rect((width * (col+1)) + gap, 1 - ((width * (row+1)) + gap), width, width);
                    image.uvRect = uv[2];
                    uv[3] = new Rect((width * col) - gap, 1 - ((width * (row+1)) + gap), width, width);
                    image.uvRect = uv[3];
                    // Assign our new UVs to the mesh.
                    //mesh.uv = uv;

                }
            }
        }
    }

  // Start is called before the first frame update
  void Start() {
    pieces = new List<Transform>();
    CreateGamePieces(0.01f);
        GameEventManager.isTouchObject = true;
    }

  // Update is called once per frame
  void Update() 
    {
        GameEventManager.isTouchObject = true;

        // Check for completion.
        if (!shuffling && CheckCompletion()) 
        {
            shuffling = true;
            StartCoroutine(WaitShuffle(0.5f));
        }

        if(win)
        {
            door.SetActive(false);
        }
    }
    public void OnClick(GameObject temp)
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i] == temp.transform)
            {
                // Check each direction to see if valid move.
                // We break out on success so we don't carry on and swap back again.
                if (SwapIfValid(i, -size, size)) { break; }
                if (SwapIfValid(i, +size, size)) { break; }
                if (SwapIfValid(i, -1, 0)) { break; }
                if (SwapIfValid(i, +1, size - 1)) { break; }
            }
        }
    }

  // colCheck is used to stop horizontal moves wrapping.
  private bool SwapIfValid(int i, int offset, int colCheck) {
    if (((i % size) != colCheck) && ((i + offset) == emptyLocation)) {
      // Swap them in game state.
      (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
      // Swap their transforms.
      (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
      // Update empty location.
      emptyLocation = i;
      return true;
    }
    return false;
  }

  // We name the pieces in order so we can use this to check completion.
  private bool CheckCompletion() {
        if (firstTime == true)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces[i].name != $"{i}")
                {
                    firstTime = false;
                    return false;
                }
            }
            return true;
        }
        else
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces[i].name != $"{i}")
                {
                    //win = true;
                    return false;
                }
            }
            GameEventManager.isTouchObject = false;
            this.gameObject.SetActive(false);
            win = true;
            return true;
        }
    }

  private IEnumerator WaitShuffle(float duration) {
    yield return new WaitForSeconds(duration);
    Shuffle();
    shuffling = false;
  }

  // Brute force shuffling.
   public void Shuffle() {
    int count = 0;
    int last = 0;
    while (count < (size * size * size)) {
      // Pick a random location.
      int rnd = Random.Range(0, size * size);
      // Only thing we forbid is undoing the last move.
      if (rnd == last) { continue; }
      last = emptyLocation;
      // Try surrounding spaces looking for valid move.
      if (SwapIfValid(rnd, -size, size)) {
        count++;
      } else if (SwapIfValid(rnd, +size, size)) {
        count++;
      } else if (SwapIfValid(rnd, -1, 0)) {
        count++;
      } else if (SwapIfValid(rnd, +1, size - 1)) {
        count++;
      }
    }
  }

    public void Close()
    {
        GameEventManager.isTouchObject = false;
        this.gameObject.SetActive (false);
    }

    public void ISuck()
    {
        foreach (var pieces in pieces)
        {
            pieces.gameObject.SetActive(false);
        }
        size = 4;
        CreateGamePieces(0.01f);
        Shuffle();
        win = true;

    }
    public void DamnISuck()
    {
        win = true;
        this.gameObject.SetActive(false);
    }
}

