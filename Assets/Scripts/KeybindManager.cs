using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
public class KeybindManager : MonoBehaviour
{
    public static KeybindManager Instance;

    public Key KeyJump { get; private set; }
    public Key KeySlide { get; private set; }

    private bool isRebinding = false;
    private Action<Key> onKeyBound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        KeyJump = PlayerPrefs.HasKey("KeyJump")
           ? (Key)System.Enum.Parse(typeof(Key), PlayerPrefs.GetString("KeyJump"))
           : Key.UpArrow;

        KeySlide = PlayerPrefs.HasKey("KeySlide")
            ? (Key)System.Enum.Parse(typeof(Key), PlayerPrefs.GetString("KeySlide"))
            : Key.DownArrow;
    }
    private void Update()
    {
        if (!isRebinding || Keyboard.current == null || onKeyBound == null) return;

        if (Keyboard.current.allKeys.Equals(null)) return;

        foreach (KeyControl key in Keyboard.current.allKeys)
        {
            if (key == null) continue; // skip any null keys
            if (key.wasPressedThisFrame)
            {
                Key newKey;
                if (Enum.TryParse(key.keyCode.ToString(), out newKey))
                {
                    onKeyBound.Invoke(newKey);
                    isRebinding = false;
                    break;
                }
            }
        }
    }


    public void BeginRebind(Action<Key> onKeyBound)
    {
        isRebinding = true;
        this.onKeyBound = onKeyBound;
    }

    public void SetKeyJump(Key newKey)
    {
        KeyJump = newKey;
        PlayerPrefs.SetString("KeyJump", newKey.ToString());
        PlayerPrefs.Save();
    }

    public void SetKeySlide(Key newKey)
    {
        KeySlide = newKey;
        PlayerPrefs.SetString("KeySlide", newKey.ToString());
        PlayerPrefs.Save();
    }

    public bool GetKeyDown(Key key)
    {
        return Keyboard.current != null && Keyboard.current[key].wasPressedThisFrame;
    }

}