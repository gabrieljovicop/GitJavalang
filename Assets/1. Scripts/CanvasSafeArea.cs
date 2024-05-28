using UnityEngine;

public class CanvasSafeArea : MonoBehaviour
{
    // Referensi ke RectTransform dari Canvas
    private RectTransform canvasRect;

    // Fungsi yang dipanggil saat skrip diaktifkan
    private void Start()
    {
        // Mendapatkan referensi ke RectTransform dari Canvas
        canvasRect = GetComponent<RectTransform>();

        // Memperbarui ukuran Canvas berdasarkan Safe Area
        UpdateCanvasSize();
    }

    // Fungsi yang dipanggil setiap kali layar berubah ukurannya
    private void Update()
    {
        // Memperbarui ukuran Canvas jika terjadi perubahan pada layar
        UpdateCanvasSize();
    }

    // Fungsi untuk memperbarui ukuran Canvas berdasarkan Safe Area
    private void UpdateCanvasSize()
    {
        // Mendapatkan informasi tentang Safe Area layar
        Rect safeArea = Screen.safeArea;

        // Mengatur ukuran dan posisi Canvas berdasarkan Safe Area
        canvasRect.offsetMin = new Vector2(safeArea.xMin, safeArea.yMin);
        canvasRect.offsetMax = new Vector2(Screen.width - safeArea.xMax, Screen.height - safeArea.yMax);
    }
}
