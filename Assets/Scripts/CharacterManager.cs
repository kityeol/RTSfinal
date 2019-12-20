using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManager : MonoBehaviour
{
    public List<GameObject> characterList;
    public ParticleSystem enemyParticle;
    public ParticleSystem clickParticle;
    bool foundCharacter = false;
    public GameObject target;
    Vector3 walkPosition;

    void Start()
    {
        characterList = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                GameObject characterGameObject = hit.collider.gameObject;
                if (characterGameObject.CompareTag("Player"))
                {
                    foundCharacter = false;
                    foreach (var item in characterList)
                    {
                        if (item == characterGameObject)
                        {
                            item.GetComponent<CharacterMove>().SetTarget(null);
                            item.GetComponent<CharacterMove>().SetDestination(item.transform.position);
                            characterGameObject.GetComponent<CharacterMove>().selected.SetActive(false);
                            characterList.Remove(item);
                            foundCharacter = true;
                            break;
                        }
                    }
                    if (foundCharacter == false)
                    {
                        characterGameObject.GetComponent<CharacterMove>().selected.SetActive(true);
                        characterList.Add(characterGameObject);
                    }
                }
                else if (characterGameObject.CompareTag("Enemy"))
                {
                    target = characterGameObject;
                    Instantiate(enemyParticle, hit.point, Quaternion.identity);
                }
                else
                {
                    Instantiate(clickParticle, hit.point, Quaternion.identity);
                    target = null;
                    walkPosition = hit.point;
                }
            }
        }

        foreach (var item in characterList)
        {
            CharacterMove move = item.GetComponent<CharacterMove>();
            move.SetTarget(target);
            if (target == null)
            {
                move.SetDestination(walkPosition);
            }
        }
    }
}
