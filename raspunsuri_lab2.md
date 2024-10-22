
LABORATOR 2

1. Axele și cubul au dispărut de pe fereastră.

3. RĂSPUNSURI LA ÎNTREBĂRI

1) Ce este un viewport?
Un viewport în OpenGL este regiunea dreptunghiulară de pe fereastra de afișare unde sunt desenate obiectele 3D.
Este specificat folosind funcția GL.Viewport(), care definește dimensiunea și poziția acestei regiuni în coordonate de pixeli.
Toate obiectele desenate sunt scalate și transformate pentru a se încadra în această zonă.

2) Ce reprezintă conceptul de frames per second (FPS) din punctul de vedere al bibliotecii OpenGL?
Frames per second (FPS) reprezintă numărul de cadre (frame-uri) desenate și afișate pe ecran într-o secundă.
În OpenGL, FPS este influențat de performanța aplicației și de cât de rapid poate aceasta să actualizeze scena și să o randeze.
Un FPS ridicat oferă o experiență vizuală mai fluidă, în timp ce un FPS scăzut poate duce la lag.

3) Când este rulată metoda OnUpdateFrame()?
Metoda OnUpdateFrame() este apelată înainte de fiecare randare a unui nou cadru, și este responsabilă pentru actualizarea logicii aplicației
(de exemplu, poziția obiectelor, interacțiunile utilizatorului etc.). Ea este executată în funcție de frecvența de update setată, care poate fi
controlată folosind parametrul UpdateFrameRate din metoda Run().

4) Ce este modul imediat de randare?
Modul imediat de randare (immediate mode) este o tehnică veche în OpenGL prin care fiecare primitiv grafic (triunghi, linie, punct etc.)
este desenat imediat ce este specificat, folosind comenzi precum GL.Begin() și GL.End(). Această metodă este simplă, dar ineficientă pentru scene complexe,
deoarece necesită multe apeluri către OpenGL și nu profită de accelerarea hardware optimă oferită de tehnici moderne (cum ar fi folosirea de vertex buffers).

5) Care este ultima versiune de OpenGL care acceptă modul imediat?
Ultima versiune majoră de OpenGL care suportă complet modul imediat de randare este OpenGL 3.0. Începând cu OpenGL 3.1,
modul imediat a fost depreciat și înlocuit cu metode mai eficiente, precum utilizarea de Vertex Buffer Objects (VBO) și Vertex Array Objects (VAO).

6) Când este rulată metoda OnRenderFrame()?
Metoda OnRenderFrame() este rulată după fiecare cadru și este responsabilă pentru randarea scenei 3D.
Aceasta este apelată în timpul fiecărui ciclu de redare și poate fi controlată folosind frecvența de randare stabilită prin metoda Run().

7) De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?
Metoda OnResize() este necesară pentru a ajusta corect viewport-ul și proiecția grafică la dimensiunile ferestrei.
Aceasta este apelată automat atunci când fereastra este redimensionată sau când este inițializată, pentru a asigura că
obiectele 3D sunt scalate și afișate corect în funcție de dimensiunea curentă a ferestrei.

8) Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia?
Metoda CreatePerspectiveFieldOfView() creează o matrice de proiecție perspectiva. Parametrii săi sunt:
	• fieldOfView: unghiul câmpului vizual pe axa verticală, măsurat în radiani. Domeniul tipic este între aproximativ 30 și 90 de grade
	  (în radiani, 0.52 la 1.57), unde unghiurile mai mici simulează un zoom, iar cele mai mari oferă o vedere largă.
	• aspectRatio: raportul dintre lățimea și înălțimea ferestrei de vizualizare (viewport). Acesta este calculat din dimensiunile ferestrei.
	• nearClipPlane: distanța până la planul de tăiere apropiat (cel mai mic z). Obiectele mai aproape de această distanță nu vor fi afișate.
          Tipic, valoarea este mai mare de 0 (ex. 0.1 sau 1.0).
	• farClipPlane: distanța până la planul de tăiere îndepărtat (cel mai mare z). Obiectele mai îndepărtate de această valoare nu vor fi afișate. 
    	  Valoarea tipică poate fi între 100 și 1000 sau mai mare, în funcție de scena 3D.


