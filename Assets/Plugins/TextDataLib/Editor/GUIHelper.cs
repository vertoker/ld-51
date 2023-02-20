using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class GUIHelper
{
    public enum Position 
    {
        TopLeft,
        TopRight,
        TopCenter,
        Center,
        BottomLeft,
        BottomRight,
        BottomCenter
    }

    private static GUIStyle m_headerLabel = null;
    private static GUIStyle m_errorLabel = null;
    public static GUIStyle HeaderLabel 
    {
        get 
        {
            if (m_headerLabel == null)
            {
                m_headerLabel = new GUIStyle(EditorStyles.boldLabel);
                m_headerLabel.alignment = TextAnchor.UpperCenter;
            }
            return m_headerLabel;
        }
    }

    public static GUIStyle ErrorLabel
    {
        get
        {
            if (m_errorLabel == null)
            {
                m_errorLabel = new GUIStyle(EditorStyles.boldLabel);
                m_errorLabel.normal.textColor = Color.red;
            }
            return m_errorLabel;
        }
    }



    public static Vector2 GetBordersFromRect(Rect src) 
    {
        return new Vector2(src.width, src.height);
    }

    public static Rect GetNewRectVertical(Vector2 offset)
    {

        Rect src = GUILayoutUtility.GetLastRect();
        Vector2 size = new Vector2(src.width, src.height);

        return GetNewRectVertical(src, size, offset, Vector2.zero, 
            Position.TopLeft);
    }

    public static Rect GetNewRectVertical(Vector2 offset,
        Vector2 borders, Position ePos = Position.TopLeft)
    {

        Rect src = GUILayoutUtility.GetLastRect();
        Vector2 size = new Vector2(src.width, src.height);

        return GetNewRectVertical(src, size, offset, borders, ePos);
    }

    public static Rect GetNewRectVertical(Vector2 size, Vector2 offset, 
        Vector2 borders, Position ePos = Position.TopLeft)
    {

        Rect src = GUILayoutUtility.GetLastRect();

        return GetNewRectVertical(src, size, offset, borders, ePos);
    }

    public static Rect GetNewRectVertical(Rect src, Vector2 size, 
        Vector2 offset, Vector2 borders, Position ePos = Position.TopLeft)
    {
        
        Vector2 v_pos = Vector2.zero;

        switch (ePos) 
        {
            case Position.TopLeft:
                v_pos = new Vector2(offset.x, src.y+src.height+offset.y);
                break;
            case Position.TopRight:
                v_pos = new Vector2(borders.x-size.x-offset.x, 
                    src.y + src.height + offset.y);
                break;
            case Position.TopCenter:
                v_pos = new Vector2((borders.x - size.x)*0.5f + offset.x,
                    src.y + src.height + offset.y);
                break;
            case Position.Center:
                v_pos = new Vector2((borders.x - size.x)*0.5f + offset.x,
                    (borders.y+src.y + src.height-size.y) *0.5f + offset.y);
                break;
            case Position.BottomLeft:
                v_pos = new Vector2(offset.x,
                    borders.y  - size.y - offset.y);
                break;
            case Position.BottomCenter:
                v_pos = new Vector2((borders.x - size.x) * 0.5f + offset.x,
                    borders.y - size.y - offset.y);
                break;
            case Position.BottomRight:
                v_pos = new Vector2(borders.x - size.x - offset.x,
                     borders.y - size.y - offset.y);
                break;
            default:
                break;
        }

        return new Rect(v_pos.x, v_pos.y,
            size.x, size.y);
    }

    public static void ShowDisableableGUI(Action GUIElementFunc, bool condition) 
    {
        GUI.enabled = condition;
        GUIElementFunc?.Invoke();
        GUI.enabled = true;
    }
}
