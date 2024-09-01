using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    
    
    public float vitesse = 5.0f;        // Vitesse de déplacement horizontal
    public float hauteurSaut = 2.0f;    // Hauteur du saut
    public float gravite = 9.81f;       // Force de gravité

    private CharacterController controller;
    private Vector3 deplacement;
    private float vitesseVerticale = 0.0f; // Vitesse verticale utilisée pour le saut et la chute

    void Start()
    {
        // Récupérer le CharacterController attaché à l'objet
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Initialiser le déplacement horizontal
        Vector3 deplacementHorizontal = Vector3.zero;

        // Vérifier les touches pour les directions sur un clavier AZERTY
        if (Input.GetKey(KeyCode.Z)) // Avancer
        {
            deplacementHorizontal += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S)) // Reculer
        {
            deplacementHorizontal += Vector3.back;
        }
        if (Input.GetKey(KeyCode.Q)) // Gauche
        {
            deplacementHorizontal += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D)) // Droite
        {
            deplacementHorizontal += Vector3.right;
        }

        // Appliquer la vitesse de déplacement horizontal
        deplacementHorizontal *= vitesse;

        // Gestion du saut et de la gravité
        if (controller.isGrounded) // Si le personnage est au sol
        {
            // Réinitialiser la vitesse verticale quand on touche le sol
            vitesseVerticale = 0.0f;

            if (Input.GetKeyDown(KeyCode.Space)) // Sauter quand la barre d'espace est pressée
            {
                vitesseVerticale = Mathf.Sqrt(2 * hauteurSaut * gravite);
            }
        }
        else // Si le personnage est en l'air, appliquer la gravité
        {
            vitesseVerticale -= gravite * Time.deltaTime;
        }

        // Ajouter le mouvement vertical au vecteur de déplacement
        deplacement = deplacementHorizontal + Vector3.up * vitesseVerticale;

        // Déplacer le personnage en utilisant le CharacterController
        controller.Move(deplacement * Time.deltaTime);
    }
}
