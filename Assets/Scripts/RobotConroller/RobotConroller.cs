using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static RobotController;
using UnityEngine.Rendering;
using System.Collections;
using System;

/// <summary>
/// �κ� 3D Object�� RobotController�� ��ư, ��ǲ�ʵ��� ������ �����δ�.
/// - Teach ��ư�� ������ �� Axis�� ���� Step���� ����ȴ�.
/// - SingleCycle, Cycle, Stop, E-Stop ��ư�� ������ �κ��� �����Ѵ�.
/// �ʿ�Ӽ�: �κ��� ���� ȸ�� �ӵ�(0~100), Duration, Min Angle, Max Angle
///                 step(speed, duration, suction, angles)
/// </summary>

public class RobotController : MonoBehaviour
{
    public class Step
    {
        public int stepNumber;
        public float speed;
        public float duration;
        public bool isSuctionOn;

        public float angleAxis1;
        public float minAngleAxis1;
        public float maxAngleAxis1;

        public float angleAxis2;
        public float minAngleAxis2;
        public float maxAngleAxis2;

        public float angleAxis3;
        public float minAngleAxis3;
        public float maxAngleAxis3;

        public float angleAxis4;
        public float minAngleAxis4;
        public float maxAngleAxis4;

        public float angleAxis5;
        public float minAngleAxis5;
        public float maxAngleAxis5;

        public Step(int _stepNuber, float _speed, float _duration, bool _isSuctionOn)
        {
            stepNumber = _stepNuber;
            speed = _speed;
            duration = _duration;
            isSuctionOn = _isSuctionOn; // �ʱ�ȭ
        }
    }
    public List<Step> steps = new List<Step>();

    [Header("Axis Pivots")]
    [SerializeField] Transform motorAxis1;
    [SerializeField] Transform motorAxis2;
    [SerializeField] Transform motorAxis3;
    [SerializeField] Transform motorAxis4;
    [SerializeField] Transform motorAxis5;

    [Header("UI ����")]
    [SerializeField] TMP_Text nowStepinfoTxt;
    [SerializeField] int totalSteps;
    [SerializeField] int currentStepsNumber;
    [SerializeField] TMP_InputField stepInput;
    [SerializeField] TMP_InputField speedInput;
    [SerializeField] TMP_InputField durationInput;
    [SerializeField] Toggle suctionToggle;
    [SerializeField] TMP_InputField angleAxis1Input;
    [SerializeField] TMP_InputField angleAxis2Input;
    [SerializeField] TMP_InputField angleAxis3Input;
    [SerializeField] TMP_InputField angleAxis4Input;
    [SerializeField] TMP_InputField angleAxis5Input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nowStepinfoTxt.text = $"Total step count: {totalSteps} / Current step: {currentStepsNumber}";

        stepInput.text = "0";
        speedInput.text = "100";
        durationInput.text = "0";
        suctionToggle.isOn = false;

        angleAxis1Input.text = "0";
        angleAxis2Input.text = "0";
        angleAxis3Input.text = "0";
        angleAxis4Input.text = "0";
        angleAxis5Input.text = "0";
    }

    // Teach ��ư�� ������ �� Axis�� ���� step���� ����ȴ�.
    public void OnTeachBtnClkEvent()
    {
        //int stepNumber;
        //bool isCorrect = int.TryParse(stepInput.text, out stepNumber);
        //if (!isCorrect)
        //{
        //    print("�ùٸ� ���� �Է��� �ּ���.");
        //    return;
        //}

        float speed;
        bool isCorrect = float.TryParse(speedInput.text, out speed);
        if (!isCorrect)
        {
            print("�ùٸ� ���� �Է��� �ּ���.");
            return;
        }

        float duration;
        isCorrect = float.TryParse(durationInput.text, out duration);
        if (!isCorrect)
        {
            print("�ùٸ� ���� �Է��� �ּ���.");
            return;
        }

        float angleAxis1;
        isCorrect = float.TryParse(durationInput.text, out angleAxis1);
        if (!isCorrect)
        {
            print("�ùٸ� ���� �Է��� �ּ���.");
            return;
        }

        float angleAxis2;
        isCorrect = float.TryParse(durationInput.text, out angleAxis2);
        if (!isCorrect)
        {
            print("�ùٸ� ���� �Է��� �ּ���.");
            return;
        }

        float angleAxis3;
        isCorrect = float.TryParse(durationInput.text, out angleAxis3);
        if (!isCorrect)
        {
            print("�ùٸ� ���� �Է��� �ּ���.");
            return;
        }

        float angleAxis4;
        isCorrect = float.TryParse(durationInput.text, out angleAxis4);
        if (!isCorrect)
        {
            print("�ùٸ� ���� �Է��� �ּ���.");
            return;
        }


        Step step = new Step(totalSteps, speed, duration, suctionToggle.isOn);
        step.angleAxis1 = angleAxis1;
        step.angleAxis2 = angleAxis2;
        step.angleAxis3 = angleAxis3;
        step.angleAxis4 = angleAxis4;

        steps.Add(step);

        totalSteps++;
        stepInput.text = totalSteps.ToString();
        nowStepinfoTxt.text = $"Total step count: {totalSteps} / Current step: {currentStepsNumber}";
        print("Step�� �߰��Ǿ����ϴ�.");
    }

    // Clear ��ư�� ������ ��� Step���� ��������.
    public void OnClearBtnClkEvent()
    {
        steps.Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // - SingleCycle, Cycle, Stop, E-Stop ��ư�� ������ �κ��� �����Ѵ�.
    public void OnSingleCycleBtnClkEvent()
    {
        // �� ���ǿ� ���� �κ��� ���Ͱ� �������� �Ѵ�.

    }

    IEnumerator RunStep(Step prevStep, Step nextStep)
    {
        Vector3 prevAxis1Euler = new Vector3(0, prevStep.angleAxis1, 0); //Axis1: Y�� �������� ȸ��
        Vector3 nextAxis1AEuler = new Vector3(0, nextStep.angleAxis1, 0);

        Vector3 prevAxis2Euler = new Vector3(0, 0, prevStep.angleAxis2); //Axis2: Z�� �������� ȸ��
        Vector3 nextAxis2AEuler = new Vector3(0, 0, nextStep.angleAxis2);

        Vector3 prevAxis3Euler = new Vector3(0, 0, prevStep.angleAxis3); //Axis: Z�� �������� ȸ��
        Vector3 nextAxis3AEuler = new Vector3(0, 0, nextStep.angleAxis3);

        Vector3 prevAxis4Euler = new Vector3(prevStep.angleAxis4, 0, 0); //Axis4: X�� �������� ȸ��
        Vector3 nextAxis4AEuler = new Vector3(nextStep.angleAxis4, 0, 0);

        Vector3 prevAxis5Euler = new Vector3(0,  0, prevStep.angleAxis5); //Axis5: Z�� �������� ȸ��
        Vector3 nextAxis5AEuler = new Vector3(0, 0, nextStep.angleAxis5);

        float currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;
            if((currentTime / nextStep.duration) > 1)
            {
                break;
            }

            motorAxis1.localRotation = RotateAngle(prevAxis1Euler, nextAxis1AEuler, currentTime / nextStep.duration);
            motorAxis2.localRotation = RotateAngle(prevAxis2Euler, nextAxis2AEuler, currentTime / nextStep.duration);
            motorAxis3.localRotation = RotateAngle(prevAxis3Euler, nextAxis3AEuler, currentTime / nextStep.duration);
            motorAxis4.localRotation = RotateAngle(prevAxis4Euler, nextAxis4AEuler, currentTime / nextStep.duration);
            motorAxis5.localRotation = RotateAngle(prevAxis5Euler, nextAxis5AEuler, currentTime / nextStep.duration);

            yield return new WaitForEndOfFrame();
        }
    }

    private Quaternion RotateAngle(Vector3 from, Vector3 to, float t)
    {
        return Quaternion.Slerp(Quaternion.Euler(from), Quaternion.Euler(to), t);
    }
}
