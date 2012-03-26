/*
 * 
 */

namespace MarioPort
{

   enum EnemyType
   {
      tpDead            =  0,
      tpDying           =  1,
      tpChibibo         =  2,
      tpFlatChibibo     =  3,
      tpDeadChibibo     =  4,
      tpRisingChamp     =  5,
      tpChamp           =  6,
      tpRisingLife      =  7,
      tpLife            =  8,
      tpRisingFlower    =  9,
      tpFlower          = 10,
      tpRisingStar      = 11,
      tpStar            = 12,
      tpFireBall        = 13,
      tpDyingFireBall   = 14,
      tpVertFish        = 15,
      tpDeadVertFish    = 16,
      tpVertFireBall    = 17,
      tpVertPlant       = 18,
      tpDeadVertPlant   = 19,
      tpRed             = 20,
      tpDeadRed         = 21,
      
      tpKoopa           = 50,
      tpSleepingKoopa   = 51,
      tpWakingKoopa     = 52,
      tpRunningKoopa    = 53,
      tpDyingKoopa      = 54,
      tpDeadKoopa       = 55,

      tpLiftStart       = 60,
      tpBlockLift       = 60,
      tpDonut           = 61,
      tpLiftEnd         = 69,
   }

   class Enemies
   {
      public const int StartEnemiesAt  = 2;
      public const int ForgetEnemiesAt = 5;

      public const int left = 0;
      public const int right = 1;
         
      public const int kGreen = 0;
      public const int kRed = 1;
      
      public const bool Turbo = false;
      
      public byte cdChamp;
      public byte cdLife;
      public byte cdFlower;
      public byte cdStar;
      public byte cdEnemy;
      public byte cdHit;
      public byte cdLift;
      public byte cdStopJump;
      
      public int PlayerX1;
      public int PlayerY1;
      public int PlayerX2;
      public int PlayerY2;
      public int PlayerXVel;
      public int PlayerYVel;
      
      public bool conStar;
      
      public Enemies()
      {
         // init FireballList and KoopaList
      }
      
      //// implementation ////
      
      /*
      {$I Chibibo.$00} {$I Chibibo.$01}
        {$I Chibibo.$02} {$I Chibibo.$03}
        {$I Champ.$00}
        {$I Poison.$00}
        {$I Life.$00}
        {$I Flower.$00}
        {$I Star.$00}
        {$I Fish.$01}
        {$I PPlant.$00} {$I PPlant.$01}
        {$I PPlant.$02} {$I PPlant.$03}
        {$I Red.$00} {$I Red.$01}

        {$I F.$00} {$I F.$01} {$I F.$02} {$I F.$03}

        {$I HIT.$00}

        {$I GrKoopa.$00} {$I GrKoopa.$01}
        {$I RdKoopa.$00} {$I RdKoopa.$01}
        {$I GrKp.$00} {$I GrKp.$01}
        {$I RdKp.$00} {$I RdKp.$01}

        {$I Lift1.$00}
        {$I Donut.$00} {$I Donut.$01}
      */
      
      private byte[,] rKoopa = new byte[4, 20 * 24 + 1];

      private const Image[] FireBallList = new Image[4];
      private const Image[,,] KoopaList = new Image[Right - Left + 1, kRed - kGreen + 1, 2];

      // {$I Fire.$00} {$I Fire.$01}

      private const int Grounded = 0;
      private const int Falling = 1;

      private const int MaxEnemies = 11;
      private const int MaxEnemiesAtOnce = 25;

      struct EnemyRec
      {
         public int Tp;
         public int SubTp;
         public int XPos;
         public int YPos;
         public int LastXPos;
         public int LastYPos;
         public int MapX;
         public int MapY;
         public int XVel;
         public int YVel;
         public int MoveDelay
         public int DelayCounter;
         public int Counter;
         public int Status;
         public byte DirCounter;
         
         public uint[] BackGrAddr = new uint[MAX_PAGE + 1];
      }

//          EnemyListPtr = ^EnemyList;
      private EnemyRec[] EnemyList = new EnemyRec[MaxEnemiesAtOnce];

//      Enemy: EnemyListPtr;
      private EnemyRec* Enemy = &EnemyList;
//      ActiveEnemies: String [MaxEnemiesAtOnce];
      private string ActiveEnemies;
//      NumEnemies: Byte absolute ActiveEnemies;
      // TODO
//      TimeCounter: Byte;
      private byte TimeCounter;

      private Image[,] EnemyPictures = new Image[MaxEnemies, Right - Left + 1];

      private void Kill (int i)
      {
//        {
//          with Enemy^[i] do
//          case Tp of
//            tpChibibo:
//            {
//              Tp := tpDeadChibibo;
//              XVel := -1 + 2 * Byte ((XPos + XVel) mod W > W div 2);
//              YVel := -4;
//              MoveDelay := 0;
//              DelayCounter := 0;
//              AddScore (100);
//            }
//            tpRed:
//            {
//              Tp := tpDeadRed;
//              XVel := -1 + 2 * Byte ((XPos + XVel) mod W > W div 2);
//              YVel := -4;
//              MoveDelay := 0;
//              DelayCounter := 0;
//              AddScore (100);
//            }
//            tpKoopa, tpSleepingKoopa, tpWakingKoopa, tpRunningKoopa:
//            {
//              Tp := tpDeadKoopa;
//              XVel := -1 + 2 * Byte ((XPos + XVel) mod W > W div 2);
//              YVel := -4;
//              MoveDelay := 0;
//              DelayCounter := 0;
//              AddScore (100);
//            }
//            tpVertFish:
//            {
//              Tp := tpDeadVertFish;
//              XVel := 0;
//              YVel := 0;
//              MoveDelay := 2;
//              DelayCounter := 0;
//              Status := Falling;
//              AddScore (100);
//            }
//            tpVertPlant:
//            {
//              Tp := tpDeadVertPlant;
//              DelayCounter := 0;
//              YVel := 0;
//              AddScore (100);
//            }
//          }
//        }
      }
      
      private void ShowStar (int X, int Y)
      {
//        Beep (100);
//        if (X + W > XView) and (X < XView + SCREEN_WIDTH) then
//          NewTempObj (tpHit, X, Y, 0, 0, W, H);
      }

      private void ShowFire (int X, int Y)
      {
        Beep (50);
        X := X - 4;
        Y := Y - 4;
        if (X + W > XView) and (X < XView + SCREEN_WIDTH) then
          NewTempObj (tpFire, X, Y, 0, 0, W, H);
      }

      private void Mirror20x24 (P1, P2: Pointer)
      {
        const
          W = 20;
          H = 24;
        type
          PlaneBuffer = array[0..H - 1, 0..W div 4 - 1] of Byte;
          PlaneBufferArray = array[0..3] of PlaneBuffer;
          PlaneBufferArrayPtr = ^PlaneBufferArray;
        var
          Source, Dest: PlaneBufferArrayPtr;
      }
        
      private void Swap (byte Plane1, byte Plane2)
      {
//          var
//            i, j: Byte;
//        {
//          for j := 0 to H - 1 do
//            for i := 0 to W div 4 - 1 do
//            {
//              Dest^[Plane2, j, i] := Source^[Plane1, j, W div 4 - 1 - i];
//              Dest^[Plane1, j, i] := Source^[Plane2, j, W div 4 - 1 - i];
//            }
//        Source := P1;
//        Dest := P2;
//        Swap (0, 3);
//        Swap (1, 2);
//      }
      }

      public void InitEnemyFigures()
      {
//      var
//        i, j: Integer;
//      {
//        if MemAvail < SizeOf (Enemy^) then
//        {
//          System.WriteLn ('Not enough memory');
//          Halt;
//        }
//        GetMem (Enemy, SizeOf (Enemy^));
//
//        Move (@Chibibo000^, EnemyPictures [1, Right], SizeOf (ImageBuffer));
//        Move (@Chibibo001^, EnemyPictures [2, Right], SizeOf (ImageBuffer));
//
//        Move (@Chibibo002^, EnemyPictures [4, Right], SizeOf (ImageBuffer));
//        Move (@Chibibo003^, EnemyPictures [5, Right], SizeOf (ImageBuffer));
//
//        Move (@Fish001^, EnemyPictures [3, Left], SizeOf (ImageBuffer));
//        Mirror (@EnemyPictures [3, Left], @EnemyPictures [3, Right]);
//
//        Move (@Red000^, EnemyPictures [6, Left], SizeOf (ImageBuffer));
//        Move (@Red001^, EnemyPictures [7, Left], SizeOf (ImageBuffer));
//
//        Move (@GrKp000^, EnemyPictures [8, Right], SizeOf (ImageBuffer));
//        Move (@GrKp001^, EnemyPictures [9, Right], SizeOf (ImageBuffer));
//
//        Move (@RdKp000^, EnemyPictures [10, Right], SizeOf (ImageBuffer));
//        Move (@RdKp001^, EnemyPictures [11, Right], SizeOf (ImageBuffer));
//
//        for i := 1 to MaxEnemies do
//          if (i in [6, 7]) then
//            Mirror (@EnemyPictures [i, Left], @EnemyPictures [i, Right])
//          else
//            if not (i in [3]) then
//              Mirror (@EnemyPictures [i, Right], @EnemyPictures [i, Left]);
//
//        for i := 0 to 1 do
//          for j := kGreen to kRed do
//            Mirror20x24 (@KoopaList [Left, j, i]^, @KoopaList [Right, j, i]^);
//      }
      }

      public void ClearEnemies()
      {
//         int i;
//           for i := 1 to MaxEnemiesAtOnce do
//             Enemy^[i]. Tp := tpDead;
//           NumEnemies := 0;
//           cdChamp := 0;
//           cdLife := 0;
//           cdFlower := 0;
//           cdStar := 0;
//           cdEnemy := 0;
//           cdHit := 0;
//           cdLift := 0;
//           cdStopJump := 0;
//         }
      }

      public void StopEnemies()
      {
//      var
//        i, j: Integer;
//      {
//        for i := 1 to NumEnemies do
//        {
//          j := Ord (ActiveEnemies [i]);
//          with Enemy^[j] do
//            case Tp of
//              tpChibibo:
//                WorldMap^[MapX, MapY] := '';
//              tpVertFish:
//                WorldMap^[MapX, MapY - 2] := '';
//              tpVertFireBall:
//                WorldMap^[MapX, MapY - 2] := '';
//              tpVertPlant:
//                WorldMap^[MapX, MapY - 2] := Chr (Ord ('') + SubTp);
//              tpRed:
//                WorldMap^[MapX, MapY] := '';
//              tpKoopa..tpRunningKoopa:
//                WorldMap^[MapX, MapY] := Chr (Ord ('') + SubTp);
//              tpBlockLift:
//                WorldMap^[MapX, MapY] := '°';
//              tpDonut:
//                WorldMap^[MapX, MapY] := '±';
//
//            }
//        }
//        ClearEnemies();
      }

      public void NewEnemy (int InitType, int SubType, int InitX, int InitY, int InitXVel, int InitYVel, int InitDelay)
      {
//      var
//        i, j: Integer;
//      {
//        if Turbo then
//        {
//          InitXVel := InitXVel * 2;
//          InitYVel := InitYVel * 2;
//          InitDelay := InitDelay div 2;
//        }
//        if InitType = tpFireBall then
//        {
//          j := 0;
//          for i := 1 to NumEnemies do
//            with Enemy^[Ord (ActiveEnemies [i])] do
//              if Tp = tpFireBall then
//                Inc (j);
//          if j >= 2 then
//            Exit;
//          StartMusic (FireMusic);
//        }
//
//        i := 1;
//        while (Enemy^[i]. Tp <> tpDead) do
//          if (i < MaxEnemiesAtOnce) then
//            Inc (i)
//          else
//            Exit;
//        with Enemy^[i] do
//        {
//          Tp := InitType;
//          SubTp := SubType;
//          MapX := InitX;
//          MapY := InitY;
//          XPos := MapX * W;
//          YPos := MapY * H;
//          XVel := InitXVel;
//          YVel := InitYVel;
//          MoveDelay := InitDelay;
//          DelayCounter := 0;
//          DirCounter := 0;
//          Status := Grounded;
//          FillChar (BackGrAddr, SizeOf (BackGrAddr), $FF);
//          Counter := 0;
//          case Tp of
//            tpVertPlant:
//              {
//                XPos := XPos + 8;
//                Status := 0;
//              }
//            tpFireBall:
//              {
//                if XVel > 0 then
//                  XPos := PlayerX2
//                else
//                  XPos := PlayerX1;
//              }
//          }
//          LastXPos := XPos;
//          LastYPos := YPos;
//        }
//        ActiveEnemies := ActiveEnemies + Chr (i);
//      }
      }

      public void ShowEnemies()
      {
//      var
//        i, j, Page: Integer;
//        Fig: Pointer;
//      {
//        Page := CurrentPage;
//        for i := 1 to NumEnemies do
//        {
//          j := Ord (ActiveEnemies [i]);
//          with Enemy^[j] do
//          if (XPos + 1 * W < XView)
//          or (XPos > XView + SCREEN_WIDTH + 0 * W)
//          or (YPos >= YView + SCREEN_HEIGHT) then
//            BackGrAddr [Page] := $FFFF
//          else
//          {
//            if Tp in [tpFireBall, tpDyingFireBall] then
//              { GetImage (XPos, YPos, W div 2, H div 2, BackGround [Page]) }
//              BackGrAddr [Page] := PushBackGr (XPos, YPos, W, H div 2)
//            else
//              { GetImage (XPos, YPos, W, H, BackGround [Page]); }
//              if Tp in [tpVertPlant, tpDeadVertPlant] then
//                BackGrAddr [Page] := PushBackGr (XPos, YPos, 24, 20)
//              else
//                if Tp in [tpKoopa..tpDeadKoopa] then
//                  BackGrAddr [Page] := PushBackGr (XPos, YPos - 10, 24, 24)
//                else
//                  BackGrAddr [Page] := PushBackGr (XPos, YPos, W + 4, H);
//
//          { if (XPos + W >= XView) and (XPos - W <= XView + NH * W) then }
//
//            case Tp of
//              tpChibibo:
//                DrawImage (XPos, YPos, W, H,
//                  EnemyPictures [1 + 3 * SubTp, Byte (DirCounter mod 32 < 16)]);
//              tpFlatChibibo:
//                DrawImage (XPos, YPos, W, H,
//                  EnemyPictures [2 + 3 * SubTp, Byte (DirCounter mod 32 < 16)]);
//              tpDeadChibibo:
//                UpSideDown (XPos, YPos, W, H, EnemyPictures [1, Left]);
//              tpRisingChamp:
//                if YPos <> (MapY * H) then
//                  if SubTp = 0 then
//                    DrawPart (XPos, YPos, W, H, 0, H - YPos mod H - 1, @Champ000^)
//                  else
//                    DrawPart (XPos, YPos, W, H, 0, H - YPos mod H - 1, @Poison000^);
//              tpChamp:
//                if SubTp = 0 then
//                  DrawImage (XPos, YPos, W, H, @Champ000^)
//                else
//                  DrawImage (XPos, YPos, W, H, @Poison000^);
//              tpRisingLife:
//                if YPos <> (MapY * H) then
//                  DrawPart (XPos, YPos, W, H, 0, H - YPos mod H - 1, @Life000^);
//              tpLife:
//                DrawImage (XPos, YPos, W, H, @Life000^);
//              tpRisingFlower:
//                if YPos <> (MapY * H) then
//                  DrawPart (XPos, YPos, W, H, 0, H - YPos mod H - 1, @Flower000^);
//              tpFlower:
//                DrawImage (XPos, YPos, W, H, @Flower000^);
//              tpRisingStar:
//                if YPos <> (MapY * H) then
//                  DrawPart (XPos, YPos, W, H, 0, H - YPos mod H - 1, @Star000^);
//              tpStar:
//                DrawImage (XPos, YPos, W, H, @Star000^);
//              tpFireBall:
//                if XPos mod 4 < 2 then
//                  DrawImage (XPos, YPos, 12, H div 2, @Fire000^)
//                else
//                  DrawImage (XPos, YPos, 12, H div 2, @Fire001^);
//              tpVertFish:
//                if (YVel <> 0) or (YPos < NV * H - H) then
//              {  if Abs (DelayCounter - MoveDelay) <= 1 then }
//                  DrawImage (XPos, YPos, W, H,
//                    EnemyPictures [3, Byte (PlayerX1 > XPos)]);
//              tpDeadVertFish:
//                if (YPos < NV * H - H) or (YVel <> 0) then
//                  UpSideDown (XPos, YPos, W, H,
//                    EnemyPictures [3, Byte (PlayerX1 <= XPos)]);
//              tpVertFireBall:
//                {
//                  if Abs (DelayCounter - MoveDelay) <= 1 then
//                  {
//                    DrawImage (XPos, YPos, W, H, FireBallList [Random (4)]^);
//                    NewGlitter (XPos + Random (W), YPos + Random (H),
//                      57 + Random (7), 14 + Random (20));
//                    NewStar (XPos + Random (W), YPos + Random (H),
//                      57 + Random (7), 14 + Random (20));
//                  }
//                }
//              tpVertPlant:
//                {
//                  if TimeCounter mod 32 < 16
//                  then
//                    case SubTp of
//                      0,
//                      1: Fig := @PPlant002;
//                      else
//                         Fig := @PPlant000
//                    end
//                  else
//                    case SubTp of
//                      0,
//                      1: Fig := @PPlant003;
//                      else
//                         Fig := @PPlant001;
//                    }
//                  DrawPart (XPos, YPos, 24, 20, 0, (MapY * H) - YPos - 1, Fig^);
//                }
//              tpDeadVertPlant:
//                {
//                  DelayCounter := 0;
//                  MoveDelay := 0;
//                  YVel := 0;
//                  inc (Status);
//                  if Status < 12 then
//                    DrawImage (XPos, YPos, 24, 20, @Hit000^)
//                  else
//                    if Status > 14 then
//                      Tp := tpDying;
//                }
//              tpRed:
//                DrawImage (XPos, YPos, W, H,
//                  EnemyPictures [6 + Byte (DirCounter mod 16 <= 8), Byte (XVel > 0)]);
//              tpDeadRed:
//                UpSideDown (XPos, YPos, W, H, EnemyPictures [6 + Byte (DirCounter mod 16 <= 8), Byte (XVel > 0)]);
//              tpKoopa:
//                DrawImage (XPos, YPos - 10, W, 24,
//                  KoopaList [Byte (XVel > 0), SubTp, Byte (DirCounter mod 16 <= 8)]^);
//              tpWakingKoopa, tpRunningKoopa:
//                DrawImage (XPos, YPos, W, H,
//                  EnemyPictures [8 + 2 * SubTp + 1 - Byte (DirCounter mod 16 <= 8), Byte (DirCounter mod 32 <= 16)]);
//              tpSleepingKoopa:
//                DrawImage (XPos, YPos, W, H,
//                  EnemyPictures [8 + 2 * SubTp, 0]);
//              tpDeadKoopa:
//                UpSideDown (XPos, YPos, W, H,
//                  EnemyPictures [8 + 2 * SubTp, Byte (DirCounter mod 16 <= 8)]);
//              tpBlockLift:
//                DrawImage (XPos, YPos, W, H, @Lift1000^);
//              tpDonut:
//                {
//                  if Status = 0 then
//                  {
//                    DrawImage (XPos, YPos, W, H, @Donut000^);
//                    if YVel = 0 then
//                      Counter := 0;
//                  end
//                  else
//                  {
//                    DrawImage (XPos, YPos, W, H, @Donut001^);
//                    Dec (Status);
//                  }
//                  if YVel > 0 then
//                    if Counter mod 24 = 0 then
//                      Inc (YVel);
//                  Inc (Counter);
//                }
//
//            }
//          }
//        }
//      }
      }
      
      public void HideEnemies()
      {
//         int i, j, Page;
//         Page := CurrentPage;
//         for i := NumEnemies downto 1 do
//         {
//           j := Ord (ActiveEnemies [i]);
//           with Enemy^[j] do
//           if (BackGrAddr [Page] <> $FFFF) then
//              PopBackGr (BackGrAddr [Page]);
//         }
      }

      private void Check (int i)
      {
//      const
//        Safe = EY1;
//        HSafe = H * Safe;
//      var
//        NewCh1, NewCh2, Ch: Char;
//        j, k, l, Side, AtX, NewX,
//        NewX1, NewX2, Y1, Y2, NewY: Integer;
//        Hold1, Hold2: Boolean;
//        X, Y: Integer;
//      {
//        with Enemy^[i] do
//        {
//          case Tp of
//            tpRisingChamp, tpRisingLife, tpRisingFlower, tpRisingStar:
//              if ((YPos / H) = (YPos div H))
//                and (YPos <> MapY * H) then
//              {
//                XVel := 1 - 2 * Byte (WorldMap^ [MapX + 1, MapY - 1] in CanHoldYou);
//                case Tp of
//                  tpRisingChamp:
//                    Tp := tpChamp;
//                  tpRisingLife:
//                    {
//                      Tp := tpLife;
//                      XVel := 2 * XVel;
//                    }
//                  tpRisingFlower:
//                    {
//                      XVel := 0;
//                      Tp := tpFlower;
//                    }
//                  tpRisingStar:
//                    {
//                      Tp := tpStar;
//                      XVel := 2 * XVel;
//                    }
//                }
//                YVel := -7;
//                MoveDelay := 1;
//                Status := Falling;
//              end
//              else
//              {
//                j := (YPos mod H);
//                if j mod 2 = 0 then
//                  Beep (130 - 20 * j);
//                Exit;
//              }
//            tpFireBall:
//              {
//                AtX := (XPos + W div 4) div W;
//                NewX := (XPos + W div 4 + XVel) div W;
//                if (AtX <> NewX) or (PlayerX1 mod W = 0) then
//                {
//                  Y1 := (YPos + H div 4 + HSafe) div H - Safe;
//                  NewCh1 := WorldMap^ [NewX, Y1];
//                  if NewCh1 in CanHoldYou then
//                    XVel := 0;
//                }
//                NewX := XPos;
//                NewY := YPos;
//                AtX := (XPos + W div 4 + XVel) div W;
//                NewY := (YPos + 2 + H div 4 + YVel + HSafe) div H - Safe;
//                NewCh1 := WorldMap^ [AtX, NewY];
//                if (YVel > 0) and (NewCh1 in CanHoldYou + CanStandOn) then
//                {
//                  YPos := ((YPos + YVel - 5 + HSafe) div H - Safe) * H;
//                  YVel := -2;
//                end
//                else
//                  if XPos mod 3 = 0 then
//                    Inc (YVel);
//                if (XVel = 0)
//                  or (NewX < XView - W)
//                  or (NewX > XView + NH * W + W)
//                  or (NewY > NV * H) then
//                {
//                  DelayCounter := - (MAX_PAGE + 1);
//                  Tp := tpDyingFireBall;
//                }
//                Exit;
//              }
//            tpStar:
//              StartGlitter (XPos, YPos, W, H);
//          }
//
//          if not (Tp in [tpVertFish, tpDeadVertFish, tpVertFireBall, tpVertPlant,
//            tpDeadVertPlant]) then
//          {
//            Side := Integer (XVel > 0) * (W - 1);
//            AtX := (XPos + Side) div W;
//            NewX := (XPos + Side + XVel) div W;
//            if (AtX <> NewX) or (Status in [Falling]) then
//            {
//              Y1 := (YPos + HSafe) div H - Safe;
//              Y2 := (YPos + HSafe + H - 1) div H - Safe;
//              NewCh1 := WorldMap^ [NewX, Y1];
//              NewCh2 := WorldMap^ [NewX, Y2];
//              Hold1 := (NewCh1 in CanHoldYou);
//              Hold2 := (NewCh2 in CanHoldYou);
//              if Hold1 or Hold2 then
//              {
//                if Tp in [tpRunningKoopa] then
//                {
//                  ShowStar (XPos + XVel, YPos);
//                  l := (YPos + HSafe + H div 2) div H - Safe;
//                  Ch := WorldMap^ [NewX, l];
//                  if (XPos >= XView) and (XPos + W <= XView + NH * W) then
//                  case Ch of
//                    'J': BreakBlock (NewX, l);
//                    '?': {
//                           case WorldMap^[NewX, l - 1] of
//                             ' ': HitCoin (NewX * W, l * H, TRUE);
//                             'à': {
//                                    if Data.Mode[Player] in [mdSmall] then
//                                      NewEnemy (tpRisingChamp, 0, NewX, l, 0, -1, 1)
//                                    else
//                                      NewEnemy (tpRisingFlower, 0, NewX, l, 0, -1, 1);
//                                  }
//                             'á': NewEnemy (tpRisingLife, 0, NewX, l, 0, -1, 2);
//                           }
//                           Remove (NewX * W, l * H, W, H, 1);
//                           WorldMap^ [NewX, l] := '@';
//                         }
//                  }
//                }
//                XVel := 0;
//              }
//            }
//
//            AtX := (XPos + XVel) div W;
//            NewX := (XPos + XVel + W - 1) div W;
//            NewY := (YPos + 1 + H + YVel + HSafe) div H - Safe;
//
//            NewCh1 := WorldMap^ [AtX, NewY];
//            NewCh2 := WorldMap^ [NewX, NewY];
//            Hold1 := (NewCh1 in CanHoldYou + CanStandOn);
//            Hold2 := (NewCh2 in CanHoldYou + CanStandOn);
//
//            if Tp in [tpLiftStart..tpLiftEnd] then
//            {
//              if (YVel <> 0) and (not (Tp in [tpDonut]))  then
//              {
//                if YVel < 0 then
//                  Hold1 := (YPos + YVel) div H < MapY;
//                if Hold1 then YVel := -YVel;
//              }
//            end
//            else
//              case Status of
//                Grounded:
//                  {
//                    if not (Hold1 or Hold2) then
//                    {
//                      Status := Falling;
//                      YVel := 1;
//                    }
//                    if (SubTp = 1) and (Tp in [tpKoopa]) then
//                    {
//                      if (XVel > 0) and (XPos mod W in [11..19]) then
//                        if (not Hold2) and Hold1 then XVel := 0;
//                      if (XVel < 0) and (XPos mod W in [1..9]) then
//                        if (not Hold1) and Hold2 then XVel := 0;
//                    }
//                  }
//                Falling:
//                  {
//                    if Hold1 or Hold2 then
//                    {
//                      Status := Grounded;
//                      YPos := ((YPos + YVel + 1 + HSafe) div H - Safe) * H;
//                      if Tp in [tpStar] then
//                      {
//                        YVel := - (5 * YVel) div 2;
//                        Status := Falling;
//                      end
//                      else
//                        YVel := 0;
//                    end
//                    else
//                    {
//                      Inc (YVel);
//                      if YVel > 4 then YVel := 4;
//                    }
//                  }
//              }
//          }
//
//          NewX1 := XPos + XVel;
//          NewX2 := NewX1 + W - 1 + 4 * Byte (Tp in [tpVertPlant]);
//          Y1 := YPos + YVel;
//          Y2 := Y1 + H - 1;
//
//          if (Tp in [tpChibibo, tpFlatChibibo, tpVertFish, tpVertPlant,
//              tpDeadVertPlant, tpRed, tpKoopa..tpRunningKoopa]) then
//            for k := 1 to NumEnemies do
//            {
//              j := Ord (ActiveEnemies [k]);
//              if (j <> i) then
//                if (Enemy^[j].Tp in [tpChibibo, tpFlatChibibo, tpRed,
//                  tpKoopa..tpRunningKoopa]) then
//                {
//                  with Enemy^[j] do
//                  {
//                    X := XPos + XVel;
//                    Y := YPos + YVel;
//                  }
//                  if (NewX1 < X + W) then
//                    if (NewX2 > X) then
//                      if (Y1 < Y + H) then
//                        if (Y2 > Y) then
//                          if Enemy^[j].Tp = tpRunningKoopa then
//                          {
//                            ShowStar (XPos, YPos);
//                            if Tp = tpRunningKoopa then
//                            {
//                              ShowStar (Enemy^[j].XPos, Enemy^[j].YPos);
//                              Kill (j);
//                            }
//                            Kill (i);
//                          end
//                          else
//                            if Tp <> tpRunningKoopa then
//                            {
//                              XVel := - XVel;
//                              Enemy^[j].XVel := - Enemy^[j].XVel;
//                              YVel := - YVel;
//                              Enemy^[j].YVel := - Enemy^[j].YVel;
//                              if Abs (X - NewX1) < W then
//                                if X > NewX1 then
//                                {
//                                  XPos := XPos - XVel;
//                                  XVel := -Abs (XVel);
//                                end
//                                else
//                                  if X < NewX1 then
//                                  {
//                                    XPos := XPos - XVel;
//                                    XVel := Abs (XVel);
//                                  }
//                            }
//                end
//                else
//                  if (Enemy^[j].Tp = tpFireBall) then
//                  {
//                    with Enemy^[j] do
//                    {
//                      X := XPos + XVel;
//                      Y := YPos + YVel;
//                    }
//                    if (NewX1 <= X + W div 2) then
//                      if (NewX2 >= X) then
//                        if (Y1 <= Y + H div 2) then
//                          if (Y2 >= Y) then
//                          {
//                            Enemy^[j].Tp := tpDyingFireBall;
//                            Enemy^[j].DelayCounter := - (MAX_PAGE + 1);
//                            ShowStar (XPos, YPos);
//                            Kill (i);
//                          }
//                  }
//            }
//
//        }
//      }
      }

      public void MoveEnemies()
      {
//      var
//        i, j, Page, NewX,
//        OldXVel, OldYVel: Integer;
//      {
//        Page := CurrentPage;
//        Inc (TimeCounter);
//        for i := 1 to NumEnemies do
//        {
//          j := Ord (ActiveEnemies [i]);
//          with Enemy^[j] do
//          {
//            Inc (DelayCounter);
//            NewX := XPos + XVel;
//            if DelayCounter > MoveDelay then
//            {
//              XPos := LastXPos;
//              YPos := LastYPos;
//              Inc (DirCounter);
//              if Tp in [tpVertFish, tpVertFireBall, tpVertPlant] then
//              {
//                if Tp = tpVertPlant then
//                {
//                  case Status of
//                    0: {
//                         case SubTp of
//                           0: if (XPos > PlayerX2 + W)
//                                or (XPos + 24 + W < PlayerX1) then
//                                  Inc (Status);
//                           1: if (XPos > PlayerX2) or (XPos + 24 < PlayerX1) then
//                                Inc (Status);
//                           2: Inc (Status);
//                         }
//                         YVel := 0;
//                         DelayCounter := 0;
//                         MoveDelay := 1;
//                       }
//                    1: {
//                         YVel := -1;
//                         DelayCounter := 0;
//                         MoveDelay := 2;
//                         if YPos + YVel <= (MapY * H - 19) then
//                         {
//                           YVel := 0;
//                           DelayCounter := 0;
//                           MoveDelay := 2;
//                           Counter := 0;
//                           Inc (Status);
//                         }
//                       }
//                    2: {
//                         Inc (Counter);
//                         if (Counter > 200)
//                         then
//                           Inc (Status);
//                         MoveDelay := 0;
//                         DelayCounter := 0;
//                       }
//                    3: {
//                         YVel := 1;
//                         DelayCounter := 0;
//                         MoveDelay := 2;
//                         if YPos > (MapY * H) then Inc (Status);
//                       }
//                    4: {
//                         YVel := 0;
//                         MoveDelay := 100 + Random (100);
//                         DelayCounter := 0;
//                         Status := 0;
//                       }
//                  }
//                end
//                else
//                  if (YPos + H >= NV * H) then
//                    if YVel > 0 then
//                    {
//                      YVel := 0;
//                      MoveDelay := 100 + Random (300);
//                      DelayCounter := 0;
//                    end
//                    else
//                    {
//                      YVel := -10;
//                      MoveDelay := 1;
//                      DelayCounter := 0;
//                      if Tp = tpVertFireBall then
//                      {
//                        Beep (100);
//                        YVel := -9;
//                      }
//                    }
//              }
//              if Tp = tpSleepingKoopa then
//              {
//                Inc (Counter);
//                if Counter > 150 then
//                {
//                  Tp := tpWakingKoopa;
//                  XVel := 1;
//                  Counter := 0;
//                }
//              }
//              if Tp = tpWakingKoopa then
//              {
//                XVel := - XVel;
//                MoveDelay := 1;
//                DelayCounter := 0;
//                Inc (Counter);
//                if (Counter > 50) then
//                {
//                  Tp := tpKoopa;
//                  if PlayerX1 > XPos then
//                    XVel := 1
//                  else
//                    XVel := -1;
//                }
//              }
//              if Tp in [tpDying, tpDyingFireBall, tpDyingKoopa] then
//                Tp := tpDead
//              else
//              if (Tp in [tpFlatChibibo])
//                or (NewX <= -W)
//                or (NewX < XView - ForgetEnemiesAt * W)
//                or (NewX > XView + NH * W + ForgetEnemiesAt * W)
//                or (YPos + YVel > NV * H)
//              then
//              {
//                case Tp of
//                  tpChibibo:
//                    WorldMap^[MapX, MapY] := '';
//                  tpVertFish:
//                    WorldMap^[MapX, MapY - 2] := '';
//                  tpVertFireBall:
//                    WorldMap^[MapX, MapY - 2] := '';
//                  tpVertPlant:
//                    WorldMap^[MapX, MapY - 2] := Chr (Ord ('') + SubTp);
//                  tpRed:
//                    WorldMap^[MapX, MapY] := '';
//                  tpKoopa..tpRunningKoopa:
//                    WorldMap^[MapX, MapY] := Chr (Ord ('') + SubTp);
//                  tpBlockLift:
//                    WorldMap^[MapX, MapY] := '°';
//                  tpDonut:
//                    WorldMap^[MapX, MapY] := '±';
//                }
//                if Tp = tpKoopa then
//                  Tp := tpDyingKoopa
//                else
//                  if Tp <> tpFireBall then
//                    Tp := tpDying
//                  else
//                    Tp := tpDyingFireBall;
//                DelayCounter := - (MAX_PAGE + 1);
//              end
//              else
//              {
//                DelayCounter := 0;
//                OldXVel := XVel;
//               { OldYVel := YVel; }
//                if Tp in [tpVertFish, tpDeadVertFish, tpVertFireBall,
//                  tpDeadVertPlant] then
//                {
//                  if (DirCounter mod 3 = 0) and (YPos + H < NV * H) then
//                    Inc (YVel);
//                }
//                if Tp in [tpDeadChibibo, tpDeadRed, tpDeadKoopa] then
//                {
//                  if XPos mod 6 = 0 then
//                    Inc (YVel);
//                end
//                else
//                  Check (j);
//                XPos := XPos + XVel;
//                YPos := YPos + YVel;
//                if XVel = 0 then
//                {
//                  XVel := - OldXVel;
//                  if Tp = tpDyingFireBall then
//                    ShowFire (XPos, YPos);
//                }
//               { if YVel = 0 then YVel := - OldYVel; }
//              }
//              LastXPos := XPos;
//              LastYPos := YPos;
//            end
//            else
//              if (XVel <> 0) or (YVel <> 0) then
//              {
//                XPos := LastXPos + (DelayCounter * XVel) div (MoveDelay + 1);
//                YPos := LastYPos + (DelayCounter * YVel) div (MoveDelay + 1);
//              }
//          }
//        }
//
//        for i := 1 to NumEnemies do
//        {
//          j := Ord (ActiveEnemies [i]);
//          with Enemy^[j] do
//          {
//            if tp in [tpChibibo, tpChamp, tpLife, tpFlower, tpStar, tpVertFish,
//                tpVertFireBall, tpVertPlant, tpRed, tpKoopa..tpRunningKoopa,
//                tpLiftStart..tpLiftEnd] then
//              if (PlayerX1 < XPos + W) then
//                if (PlayerX2 > XPos) then
//                  if (PlayerY1 + PlayerYVel < YPos + H) then
//                    if (PlayerY2 + PlayerYVel > YPos) then
//                    {
//                      if Star then
//                        if not (Tp in [tpLiftStart..tpLiftEnd])
//                        then
//                        {
//                          Beep (800);
//                          Kill (j);
//                          cdHit := 1;
//                        }
//                      case Tp of
//                        tpSleepingKoopa, tpWakingKoopa:
//                          {
//                            Tp := tpRunningKoopa;
//                            XVel := 5 * (2 * Byte (XPos > PlayerX1) - 1);
//                            MoveDelay := 0;
//                            DelayCounter := 0;
//                            Beep (800);
//                            cdEnemy := 1;
//                            AddScore (100);
//                          }
//                        tpChamp:
//                          {
//                            if SubTp = 0 then
//                            {
//                              cdChamp := $1;
//                              AddScore (1000);
//                            end
//                            else
//                              cdHit := 1;
//                            Tp := tpDying;
//                            DelayCounter := - (MAX_PAGE + 1);
//                            CoinGlitter (XPos, YPos);
//                          }
//                        tpLife:
//                          {
//                            cdLife := $1;
//                            Tp := tpDying;
//                            DelayCounter := - (MAX_PAGE + 1);
//                            CoinGlitter (XPos, YPos);
//                            AddScore (1000);
//                          }
//                        tpFlower:
//                          {
//                            cdFlower := $1;
//                            Tp := tpDying;
//                            DelayCounter := - (MAX_PAGE + 1);
//                            CoinGlitter (XPos, YPos);
//                            AddScore (1000);
//                          }
//                        tpStar:
//                          {
//                            cdStar := $1;
//                            Tp := tpDying;
//                            DelayCounter := - (MAX_PAGE + 1);
//                            CoinGlitter (XPos, YPos);
//                            AddScore (1000);
//                          }
//                        tpVertFireBall:
//                          {
//                            cdHit := 1;
//                          }
//
//                      else
//                        if ((PlayerYVel > YVel) or (PlayerYVel > 0))
//                          and (PlayerY2 <= YPos + H) then
//                        {
//                          case Tp of
//                            tpChibibo:
//                              {
//                                Tp := tpFlatChibibo;
//                                XVel := 0;
//                                DelayCounter := - 2 - 15 * Byte (YVel = 0);
//                                Beep (800);
//                                cdEnemy := 1;
//                                AddScore (100);
//                              }
//                            tpVertFish:
//                              if (YPos + H < NV * H) then
//                              {
//                                Kill (j);
//                                Beep (800);
//                                cdEnemy := 1;
//                              }
//                            tpKoopa, tpRunningKoopa:
//                              {
//                                Tp := tpSleepingKoopa;
//                                XVel := 0;
//                                Counter := 0;
//                                Beep (800);
//                                cdEnemy := 1;
//                                AddScore (100);
//                              }
//                            tpLiftStart..tpLiftEnd:
//                              {
//                                if Tp = tpDonut then
//                                {
//                                  Status := 2;
//                                  if (Counter > 20) and (YVel = 0) then
//                                    Inc (YVel);
//                                }
//                                cdStopJump := Byte (PlayerYVel <> 2);
//                                cdLift := 1;
//                                PlayerY1 := YPos - 2 * H;
//                                PlayerY2 := YPos - 1;
//                                PlayerXVel := XVel;
//                                if MoveDelay <> 0 then
//                                  PlayerXVel := XVel * XPos mod 2;
//                                PlayerYVel := YVel;
//                              }
//
//
//                          }
//                        end
//                        else
//                          if (not
//                            ((Tp = tpVertFish)
//                              and (not (Abs (DelayCounter - MoveDelay) <= 1))
//                            or (Tp in [tpLiftStart..tpLiftEnd])))
//                          then
//                          {
//                            cdHit := 1;
//                            if Star then
//                              Kill (j);
//                          }
//                      }
//                    }
//          }
//        }
//
//        i := 1;
//        while i <= Length (ActiveEnemies) do
//          if Enemy^[Ord (ActiveEnemies [i])].Tp = tpDead then
//            Delete (ActiveEnemies, i, 1)
//          else
//            Inc (i);
//        NumEnemies := Length (ActiveEnemies);
//      }
      }

      public void StartEnemies (int X, short Dir)
      {
//      var
//        i: Integer;
//        Remove: Boolean;
//      {
//        if (X < 0) or (X > Options.XSize) then Exit;
//        for i := 0 to NV - 1 do
//        {
//          Remove := TRUE;
//          Case WorldMap^[X, i] of
//            '': NewEnemy (tpChibibo, 0, X, i, 1 * Dir, 0, 2);
//            '': NewEnemy (tpVertFish, 0, X, (i + 2), 0, 0, 50 + Random (100));
//            '': NewEnemy (tpVertFireBall, 0, X, (i + 2), 0, 0, 50 + Random (100));
//            '': NewEnemy (tpChibibo, 1, X, i, 1 * Dir, 0, 2);
//            '',
//            '
//      
//            '': NewEnemy (tpVertPlant, Ord (WorldMap^[X, i]) - Ord (''), X, (i + 2),
//                   0, 0, 20 + Random (50));
//            '': NewEnemy (tpRed, 0, X, i, 1 * Dir, 0, 2);
//            '',
//            '',
//            '': NewEnemy (tpKoopa, Ord (WorldMap^[X, i]) - Ord (''), X, i,
//                   Dir, 0, 2);
//            '°': if (WorldMap^[X - 1, i] in CanHoldYou)
//                   or (WorldMap^[X + 1, i] in CanHoldYou)
//                 then
//                   NewEnemy (tpBlockLift, 0, X, i, -Dir, 0, 0)
//                 else
//                   NewEnemy (tpBlockLift, 0, X, i, 0, -Dir, 0);
//            '±': NewEnemy (tpDonut, 0, X, i, 0, 0, 0);
//            else
//              Remove := FALSE;
//          }
//          if Remove then WorldMap^[X, i] := ' ';
//        }
//      }
      }
      
      public void HitAbove (int MapX, int MapY)
      {
//      var
//        i, j, X, Y: Integer;
//      {
//        Y := MapY * H;
//        X := MapX * W;
//        for i := 1 to NumEnemies do
//        {
//          j := Ord (ActiveEnemies [i]);
//          with Enemy^[j] do
//            if YPos = Y then
//              if (XPos + XVel + W > X) and (XPos + XVel < X + W) then
//                case Tp of
//                  tpChamp, tpLife, tpFlower, tpStar, tpKoopa..tpWakingKoopa:
//                    {
//                      if ((XVel > 0) and (XPos + XVel + W div 2 <= X)) or
//                        ((XVel < 0) and (XPos + XVel + W div 2 >= X)) then
//                        XVel := -XVel;
//                      YVel := -7;
//                      Status := Falling;
//                      if Tp in [tpKoopa..tpWakingKoopa] then
//                      {
//                        Tp := tpSleepingKoopa;
//                        XVel := 0;
//                      }
//                    }
//                  tpChibibo, tpRed:
//                    Kill (j);
//                }
//        }
//      }
      }

   }

}
